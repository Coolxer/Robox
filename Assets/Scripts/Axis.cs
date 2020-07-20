using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// funkcja reprezentujaca pojedyncza os
// zawiera cale centrum sterowania dana osia
public class Axis : MonoBehaviour
{
    // obiekt typu ValueController umozliwiajacy zmiane pozycji ramienia
    public ValueController position;

    // obiekt typu ValueController umozliwiajacy zmiane predkosci ruchu ramienia
    public ValueController speed;

    // obiekt typu Button wlaczajacy/wylaczajacy test ruchu osi
    public Button autoButton;

    // obiekt typu Text reprezentujacy pole tekstowe z aktualnym kierunkiem ruchu
    public Text autoText;

    // wektor trojwymiarowy okreslajacy na ktorej osi "pracuje" os robota
    public Vector3 vector;

    // wektor definiujacy poczatkowe odsuniecie wezla od celu
    public Vector3 startOffset;

    // zmienna okreslajaca czy test osi jest uruchomiony czy nie
    private bool auto = false;

    // zmienna definiujaca aktualny kierunek ruchu podczas testu osi
    private int direction = 1;

    void Awake()
    {
        // obliczenie odsuniecia osi od celu
        startOffset = transform.localPosition;
    }

    // funkcja jest uruchamiana przed pierwszm update'm
    void Start()
    {   
        // dodanie funkcji obslugujacej zdarzenie klikniecia przycisku Auto/stop
        autoButton.onClick.AddListener(setAutoRotate);
    }

    // funkcja uruchamiana jest raz na klatke
    void FixedUpdate()
    {
        // jesli test osi jest uruchomiony
        if(auto) autoRotate();
        // jesli wartosc suwaka ulegla zmianie
        else if (position.delta != 0)
        {
            // dokonaj rotacji osi wedle zadanego wektora ruchu
            transform.localEulerAngles = vector * position.getValue();
            
            // zresetowanie wartosci po dokonanym ruchu
            position.delta = 0;   
        }
    }

    // funkcja uruchamiana po kliknieciu przycisku auto/stop
    public void setAutoRotate()
    {
        // negacja wartosci bitowej
        auto = !auto;

        string text;
        Color color;

        // ponizsze dwa warunki obsluguja 2 szczegolne przypadki,
        // kiedy slider jest na pozycji min lub max
        // wtedy nalezy konkretnie zdefiniowac  kierunek

        // sprawdzenie czy slider znajduje sie na minimum
        if(position.getValue() == position.getMin())
            // ustawienie kierunku na 1
            direction = 1;
         // sprawdzenie czy slider znajduje sie na maksimum
        else if (position.getValue() == position.getMax())
            // ustawienie kierunku na -1
            direction = -1;

        // jesli test osi zostal deaktywowany powyzej to nalezy wyswietlic przycisk AUTO
        if(!auto)
        {
            // przygotowanie tekstu "AUTO"
            text = "AUTO";

            // przygotowanie koloru zielonego
            color = Color.green;

            // zmiana koloru pola tekstowego kierunku na szary
            autoText.color = new Color(0.25f, 0.25f, 0.25f);

            // zmiana watosci pola tekstowego kierunku na ---
            autoText.text = "---";
        }
        else // jesli test osi zostal aktywowany powyzej to nalezy wyswietlic przycisk STOP
        {
            // przygotowanie tekstu "STOP"
            text = "STOP";

            // przygotowanie koloru czerwonego
            color = Color.red;

            // ustawienie koloru w zaleznosci od kierunku
            // jesli kierunek to -1 to kolor to czerwony inaczej zielony
            autoText.color = direction == -1 ? Color.red : Color.green;

            // ustawienie tekstu w zaleznosci od kierunku
            // jesli kierunek to -1 to tekst to LEFT inaczej RIGHT
            autoText.text = direction == -1 ? "LEFT" : "RIGHT";

        }

        // ustawienie tekstu przycisku
        autoButton.GetComponentInChildren<Text>().text = text;

        // zmiana koloru przycisku
        autoButton.GetComponent<Image>().color = color;
    }

    // funkcja obslugujaca automatyczny ruch osi
    private void autoRotate()
    {
        // jesli zostala osiagnieta maksymalna badz minimalna pozycja osi to zmien kierunek
        if(position.getValue() <= position.getMin() || position.getValue() >= position.getMax()) 
            changeDirection();

        // obliczenie nowej wartosci dla osi, korzystajac z poprzedniej wartosci, kierunku
        float newValue = position.getValue() + (speed.getValue() * Time.deltaTime * direction);

        moveTo(newValue);
    }

    // funkcja zmieniajaca kierunek automatycznego ruchu
    private void changeDirection()
    {
        // jesli direction bylo rowne 1 to bedzie rowne -1
        // jesli direction bylo rowne -1 to bedzie rowne 1
        direction *= -1;

        if(direction == -1)
        {
            autoText.text = "LEFT";
            autoText.color = Color.red;            
        }
        else if (direction == 1)
        {
            autoText.text = "RIGHT";
            autoText.color = Color.green;
        }
    }

    // funkcja zwracajaca aktualny kat dla wezla zaleznie od osi obrotu
    public float getAngle()
    {
        // jesli wektor obrotu to [1, 0, 0] (os X)
        if(vector == Vector3.right)
            return transform.localEulerAngles.x;
        // jesli wektor obrotu to [0, 1, 0] (os Y)
        else if (vector == Vector3.up)
            return transform.localEulerAngles.y;
        // jesli wektor obrotu to [0, 0, 1] (os Z)
        else 
            return transform.localEulerAngles.z;
    }

    // funkcja ustawiajaca wartosc obrotu na podstawie podanej wartosci i ustalonej osi obrotu
    public void setAngle(float angle)
    {
        // jesli wektor obrotu to [1, 0, 0] (os X)
        if(vector == Vector3.right)
            transform.localEulerAngles = new Vector3(angle, 0, 0);
        // jesli wektor obrotu to [0, 1, 0] (os Y)
        else if (vector == Vector3.up)
            transform.localEulerAngles = new Vector3(0, angle, 0);
        // jesli wektor obrotu to [0, 0, 1] (os Z)
        else transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void moveTo(float pos)
    {
        // zmiana wartosci suwaka
        position.setValue(pos);

        // rotacja osi
        transform.localEulerAngles = vector * pos;
    }
}
