using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// klasa reprezentujaca kontroler wartosci
// zarzadzanie wartoscia polega na sledzeniu wartosci Slider'a (suwaka)
// oraz aktualizowanie wyswietlanej wartosci w polu
public class ValueController : MonoBehaviour
{
    // obiekt typu Slider reprezentujacy suwak umozliwiajacy zmiane wartosci od min do max
    private Slider slider;

    // obiekt typu Text to pole, w ktorym wyswietlana jest aktualna wartosci suwaka
    private Text text;

    // ostatnio odczytana wartosc 
    private float lastValue;

    public float delta;

    // funkcja uruchamiana podczas ladowania skryptu
    void Awake()
    {
        // pobranie slider'a sposrod komponentow obiektu
        slider = GetComponentInChildren<Slider>();

        // pobranie text'u o nazwie "value" sposrod komponentow obiektu
        text = transform.Find("value").GetComponent<Text>();
    }

    // funkcja jest uruchamiana przed pierwszym update'm
    void Start()
    {
        lastValue = slider.value;

        // dodanie funkcji obslugujacej zmiane wartosci suwaka
        slider.onValueChanged.AddListener(
            delegate { onSlide();}
        );
    }

    // funkcja zwracajaca aktualna wartosc suwaka
    public float getValue()
    {
        return slider.value;
    }

    public float getLastValue()
    {
        return lastValue;
    }

    // funkcja zwracajaca minimalna wartosc suwaka
    public float getMin()
    {
        return slider.minValue;
    }

    // funkcja zwracajaca maksymalna wartosc suwaka
    public float getMax()
    {
        return slider.maxValue;
    }

    private void onSlide()
    {
        // obliczenie delty
        delta = slider.value - lastValue;
        
        // zmiana wartosci pola tekstowego
        text.text = slider.value.ToString();

        // aktualizacja ostatniej wartosci
        lastValue = slider.value;
    }

    // funkcja uruchamiana podczas zmiany wartosci suwaka, a takze wywolywana z "zewnatrz"
    public void setValue(float value)
    {
        // ustawienie wartosci suwaka na podana wartosc
        slider.value = value;

        // zmiana wartosci pola tekstowego
        text.text = value.ToString();

        // obliczenie delty
        delta = value - lastValue;

        // zapisanie aktualnej wartosci jako poprzednia
        lastValue = slider.value;
    }
}

