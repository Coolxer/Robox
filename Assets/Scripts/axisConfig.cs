using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// funkcja reprezentujaca pojedyncza os
// zawiera cale centrum sterowania dana osia
public class AxisConfig : MonoBehaviour
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

    // zmienna okreslajaca czy test osi jest uruchomiony czy nie
    private bool auto = false;

    // zmienna definiujaca aktualny kierunek ruchu podczas testu osi
    private int direction = 1;

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
        else if (position.getDelta() != 0)
        {
            // dokonaj rotacji osi wedle zadanego wektora ruchu o delte
            transform.Rotate(vector * position.getDelta());
            // zresetuj delte
            position.resetDelta();
        }
    }

    // funkcja uruchamiana po kliknieciu przycisku auto/stop
    public void setAutoRotate()
    {
        // negacja wartosci bitowej
        auto = !auto;

        string text;
        Color color;

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

            // zmiana koloru pola tekstowego kierunku na
            // zielony jest kierunek jest rowny 1
            // czerwony jesli kierunek jest rowny -1
            autoText.color = direction == -1 ? Color.red : Color.green;

            // zmiana watosci pola tekstowego kierunku na
            // RIGHT jesli kierunek jest rowny 1
            // LEFT jesli kierunek jest rowny -1
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
         if(position.getValue() >= position.getMax() || position.getValue() <= position.getMin()) 
            changeDirection();

        // obliczenie nowej wartosci dla osi, korzystajac z poprzedniej wartosci, kierunku
        // zmiana wartosci zalezy od uplywu czasu i zadanej predkosci
        float newValue = position.getLastValue() + (speed.getValue() * Time.deltaTime * direction);

        // obliczenie delty
        float delta = newValue - position.getLastValue();

        // transformacja osi wedle zadanego wektora o delte
        transform.Rotate(vector * delta);

        // zmiana wartosci suwaka pozycji
        position.setValue(newValue);
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
}
