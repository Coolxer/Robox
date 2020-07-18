using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// klasa zarzadzajaca zdarzeniem obracania kamery wokol robota
// jest klasa stricte techniczna i wlacza lub wylacza stan przenoszenia
// zdarzenie jest aktywne dopoki uztykownik trzyma wcisniety lewy przycisk myszy
public class DraggingManager : MonoBehaviour
{
    // zmienna definujace aktywnosc zdarzenia
    private bool dragging = false;

    // funkcja obslugujaca zdarzenie klikniecia przycisku na tym obiekcie
    void OnMouseDown()
    {
        // wlaczenie zdarzenia przesuwania myszka
        dragging = true;
    }

    // funkcja uruchamiana w petli dla tej klasy
    void Update()
    {
        // jesli puszczono lewy przycisk myszki
        if(Input.GetMouseButtonUp(0))
        {
            // jesli zdarzenie bylo aktywne
            if(dragging)
                // to deaktywuj to zdarzenie
                dragging = false;
        }
    }

    // typowy getter dla klasy
    // funkcja zwraca aktualny stan zdarzenia "przenoszenie"
    public bool isDragging()
    {
        return dragging;
    }
}
