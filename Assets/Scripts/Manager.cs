using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// klasa reprezentujaca menadzer programu
// obsluguje ona glowne przyciski dolnego menu
public class Manager : MonoBehaviour
{
    // obiekt typu Button sluzacy do restartu aplikacji
    public Button restartBtn;

    // obiekt typu Button sluzacy do wlaczenia/wylaczenia trybu inverse kinematics
    public Button inverseBtn;

    // obiekt typu Button sluzacy pokazywania i chowania menu bocznych
    public Button showHideBtn;

    // obiekt typu Button sluzacy do wylaczenia aplikacji
    public Button exitBtn;

    // obiekt reprezentujacy lewe menu
    public GameObject leftPanel;

    // obiekt reprezentujacy prawe menu
    public GameObject rightPanel;
    
    // zmienna okreslajaca czy boczne menu sa aktualnie widoczne
    private bool visibleSides = true;

    private bool inverseEnabled = false;

    // funkcja jest uruchamiana przed pierwszym update'm
    void Start()
    {
        // dodanie funkcji obslugujacych klikniecia poszegolnych przyciskow
        restartBtn.onClick.AddListener(restart);
        inverseBtn.onClick.AddListener(inverse);
        showHideBtn.onClick.AddListener(showHide);
        exitBtn.onClick.AddListener(exit);
    }

    // funkcja obslugujaca klikniecie przycisku restart
    private void restart() 
    {
        // zaladowanie aktualnej sceny od nowa
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    // funkcja obslugujaca klikniecie przycisku inverse
    private void inverse() 
    {
        inverseEnabled = !inverseEnabled;
    }

    // funkcja obslugujaca klikniecie przycisku showHide
    private void showHide() 
    {
        // negacja wartosci binarnej
        visibleSides = !visibleSides;

        // aktywacja badz deaktywacja paneli bocznych w zaleznosci od wartosci zmiennej visibleSides
        leftPanel.SetActive(visibleSides);
        rightPanel.SetActive(visibleSides);

        // zmiana tekstu przycisku w zaleznosci od wartosci zmiennej
        // jesli visibleSides jest true (panele boczne sa widoczne) to tekst to HIDE (mozliwosc schowania paneli)
        // jestli visibleSide jest false (panele boczne nie sa widoczne) to tekst to SHOW (mozliwosc pokazania paneli)
        showHideBtn.GetComponentInChildren<Text>().text = visibleSides ? "HIDE" : "SHOW";
    }

    // funkcja obslugujaca klikniecie przycisku exit
    private void exit()
    {
        SceneManager.LoadScene("Credits");

        // zamkniecie aplikacji
        //Application.Quit();
    }
}
