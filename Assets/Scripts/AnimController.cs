using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// klasa reprezentujaca kontroler animacji napisow koncowych
public class AnimController : MonoBehaviour
{
    // funkcja jest reakcja na zdarzenie konca animacji
    public void onAnimationFinished()
    {
        // zamkniecie aplikacji
        Application.Quit();
    }
}
