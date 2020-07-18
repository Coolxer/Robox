using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// klasa reprezentujaca manadzer kamer
// umozliwia ona zmiane aktualnej kamery
// przy pomocy przycisku
public class CameraManager : MonoBehaviour
{
    // obiekt typu Button (przycisk)
    public Button camButton;

    // indeks aktualnej kamery
    private int currentCameraIndex = 0;
    
    // indeks poprzedniej kamery
    private int lastCameraIndex = 0;

    // tablica obiektow typu Camera, przechowujaca 5 kamer
    private Camera[] cameras = new Camera[5];

    // funkcja Start uruchamiana jest przed pierwszym update'm
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            // wypelnienie tablicy kamer, uzywajac wyszukiwania po nazwie komponentow. Kamery maja nazwy "camX", gdzie X to numer
            cameras[i] = transform.Find("cam" + i.ToString()).GetComponent<Camera>();

            // deaktywacja poszczegolnych kamer na starcie
            cameras[i].enabled = false;
        }

        // aktywacja pierwszej kamery
        cameras[currentCameraIndex].enabled = true;

        // dodanie funkcji obslugujacej zdarzenie klikniecia przycisku camX
        camButton.onClick.AddListener(changeCamera);
    }

    // funkcja wywolywana po kliknieciu na przycisk
    // Oblicza nowy indeks kamery, aktywuje ja i zmienia tekst przycisku
    private void changeCamera()
    {
        // zapisanie aktualnego indeksu jako ostatni
        // poniewaz aktualny bedzie zmieniany
        lastCameraIndex = currentCameraIndex;

        // jesli indeks jest ostatnim w tablicy ( 4 )
        if(currentCameraIndex == cameras.Length - 1)
            currentCameraIndex = 0; // to ustaw go na 0 (od poczatku)
        else // inaczej zwieksz indeks o 1
            currentCameraIndex++;

        // deaktywacja "starej" kamery
        cameras[lastCameraIndex].enabled = false;
        // aktywacja "nowej" kamery
        cameras[currentCameraIndex].enabled = true;

        // zmiana tesktu przycisku, tak aby wyswietlal numer nastepnej kamery
        // czyli tej, ktora zostanie aktywowana po kliknieciu przycisku
        camButton.GetComponentInChildren<Text>().text = "CAM " + (currentCameraIndex + 1).ToString();
    }
}
