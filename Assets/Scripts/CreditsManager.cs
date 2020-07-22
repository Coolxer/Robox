using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// klasa reprezentujaca menedzer sceny za napisami koncowymi
public class CreditsManager : MonoBehaviour
{
    // funkcja bedaca reakcja na klikniecie myszy badz koniec animacji
    public void Exit()
    {
        // zamkniecie aplikacji
        Application.Quit();
    }
}
