using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// klasa reprezentujaca ruch obiektu po wyznaczonych sciezkach w trybie "Inverse Kinematics"
public class TargetMovement : MonoBehaviour
{

    // os X
    public GameObject xAxis;

    // os Y
    public GameObject yAxis;

    // os Z
    public GameObject zAxis;

    // kontroler pozycji dla osi X
    public ValueController xPos;

    // kontroler pozycji dla osi Y
    public ValueController yPos;

    // kontroler pozycji dla osi Z
    public ValueController zPos;

    // funkcja jest uruchamiana raz na klatke
    void Update()
    {
        // jesli obiekt nie jest aktywny nie bedzie podjeta zadna akcja
        // obiekt jest aktywny tylko w trybie "Inverse Kinematics"
        if(!gameObject.activeInHierarchy) return;

        // jesli zostala zmieniona wartosc na sliderze osi X
        if (xPos.delta != 0)
        {
            // zmiana pozycji dla obiektu i osi Y i Z
            transform.position = new Vector3(xPos.getValue(), transform.position.y, transform.position.z);
            yAxis.transform.position = new Vector3(xPos.getValue(), yAxis.transform.position.y, yAxis.transform.position.z);
            zAxis.transform.position = new Vector3(xPos.getValue(), zAxis.transform.position.y, zAxis.transform.position.z);

            // zresetowanie wartosci po dokonanym ruchu
            xPos.delta = 0; 
        }
        // jesli zostala zmieniona wartosc na sliderze osi Y
        else if (yPos.delta != 0)
        {
            // zmiana pozycji dla obiektu i osi X i Z
            transform.position = new Vector3(transform.position.x, yPos.getValue(), transform.position.z);
            xAxis.transform.position = new Vector3(xAxis.transform.position.x, yPos.getValue(), xAxis.transform.position.z);
            zAxis.transform.position = new Vector3(zAxis.transform.position.x, yPos.getValue(), zAxis.transform.position.z);

            // zresetowanie wartosci po dokonanym ruchu
            yPos.delta = 0; 
        }
        // jesli zostala zmieniona wartosc na sliderze osi Z
        else if (zPos.delta != 0)
        {
            // zmiana pozycji dla obiektu i osi X i Y
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos.getValue());
            xAxis.transform.position = new Vector3(xAxis.transform.position.x, xAxis.transform.position.y, zPos.getValue());
            yAxis.transform.position = new Vector3(yAxis.transform.position.x, yAxis.transform.position.y, zPos.getValue());

            // zresetowanie wartosci po dokonanym ruchu
            zPos.delta = 0; 
        }
    }

    // funkcja resetuje pozycje osi i obiektu
    public void reset()
    {
        // reset pozycji obiektu do poczatkowej
        transform.position = new Vector3(-85, 588, 1000);

        // ustawienie wartosci na suwakach i polach tekstowych
        xPos.setValue(227);
        yPos.setValue(540);
        zPos.setValue(1000);

        // wylaczenie update'u
        xPos.delta = yPos.delta = zPos.delta = 0;

        // reset pozycji osi
        xAxis.transform.position = new Vector3(227, 588, 1000);
        yAxis.transform.position = new Vector3(-85, 540, 1000);
        zAxis.transform.position = new Vector3(-85, 588, 1000);
    }
}
