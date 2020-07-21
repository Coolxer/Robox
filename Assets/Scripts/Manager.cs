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

    // obiekt reprezentujacy lewe i prawe menu
    public GameObject content;

    // tablica osi sluzaca m.in do resetu pozycji wszystkich osi
    public Axis[] axes = new Axis[6];

    // obiekt obslugujacy logike kinematyki odwrotnej
    public GameObject ik;

    // panel sterujacy kinematyki odwrotnej
    public GameObject ikPanel;
    
    // zmienna okreslajaca czy boczne menu sa aktualnie widoczne
    private bool visibleSides = true;

    // zmienna okreslajaca czy kinematyka odwrotna jest aktywna
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
        SceneManager.LoadScene("Main");
    }

    // funkcja obslugujaca klikniecie przycisku inverse
    private void inverse() 
    {
        inverseEnabled = !inverseEnabled;

        if(inverseEnabled)
        {
            // zmiana tekstu przycisku
            inverseBtn.GetComponentInChildren<Text>().text = "EXHIBITION";

            // deaktywacja paneli bocznych
            content.SetActive(false);

            // aktywacja panelu kinematyki odwrotnej
            ikPanel.SetActive(true);
            
            // aktywacja kinematyki odwrotnej
            ik.SetActive(true);

            resetPositions();
        }
        else
        {
            // zmiana tekstu przycisku
            inverseBtn.GetComponentInChildren<Text>().text = "IK MOVE";

            // aktywacja paneli bocznych
            content.SetActive(true);

            // deaktywacja panelu kinematyki odwrotnej
            ikPanel.SetActive(false);

            // deaktywacja kinematyki odwrotnej
            ik.SetActive(false);

            resetPositions();
        }
    }

    // funkcja obslugujaca klikniecie przycisku showHide
    private void showHide() 
    {
        // negacja wartosci binarnej
        visibleSides = !visibleSides;

        // aktywacja badz deaktywacja paneli bocznych w zaleznosci od wartosci zmiennej visibleSides
        content.SetActive(visibleSides);

        // zmiana tekstu przycisku w zaleznosci od wartosci zmiennej
        // jesli visibleSides jest true (panele boczne sa widoczne) to tekst to HIDE (mozliwosc schowania paneli)
        // jestli visibleSide jest false (panele boczne nie sa widoczne) to tekst to SHOW (mozliwosc pokazania paneli)
        showHideBtn.GetComponentInChildren<Text>().text = visibleSides ? "HIDE" : "SHOW";
    }

    // funkcja obslugujaca klikniecie przycisku exit
    private void exit()
    {
        SceneManager.LoadScene("Credits");
    }

    private void resetPositions()
    {
        for(int i = 0; i < axes.Length; i++)
            axes[i].moveTo(0);
    }
}
