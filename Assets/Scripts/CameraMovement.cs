using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// klasa reprezentujaca ruch kamery 0
// Ruch oznacza tutaj obrot wokol zadanego punktu
// oraz przyblizanie i oddalanie widoku
public class CameraMovement : MonoBehaviour
{
    // obiekt definiujacy przestrzen robocza
    // okresla on pole, na ktorych mozliwy jest ruch kamery przy pomoca myszki
    // pozwala uniknac kolizji z GUI
    public GameObject workspace;

    // obiekt okreslany jako target to obiekt wokol ktorego mozliwy jest ruch kamery
    // w tym przypadku target'em jest robot, a dokladniej jego centralny punkt
    public GameObject target;

    // zmienna okreslajaca predkosc (czulosc) przyblizania i oddalania kamery przy pomocy scroll'a
    public float zoomspeed = 1000.0f;

    // zmienna okreslajaca predkosc (czulosc) rotacji kamery wokol punktu
    public float rotatespeed = 10.0f;

    // obiekt draggingManager zarzadzajacy stanem przenoszenia przy uzyciu myszki
    private DraggingManager draggingManager;

    // funkcja uruchamiana przed pierwszym frame'm
    void Start()
    {
        // pobranie draggingManager ze zbioru komponentow z workspace'a
        draggingManager = workspace.GetComponent<DraggingManager>();
    }

    // funkcja update jest uruchamiana raz na klatke
    void Update()
    {
        // nadsluchiwanie zdarzenia rotacji kamery wokol punktu
        rotate();

        // ndasluchiwanie zdarzenia przyblizania/oddalania kamery
        //zoom(); 
    }

    // funkcja obslugujaca zdarzenie rotacji kamery wokol punktu
    private void rotate()
    {
        // jesli kamera jest nieaktywna to wyjdz z funkcji
        // ten skrypt przypisany jest tylko do kamery 0
        // zatem ruch kamery jest mozliwy tylko na tej kamerze
        if(!GetComponent<Camera>().enabled) return;
            
        // sprawdzenie aktualnego stanu "przenoszenia". Jesli jest aktywny to
        if (draggingManager.isDragging())
            // rotacja kamery wokol punktu  uzywajac osi poziomej myszy i zadanej predkosci obrotowej
            transform.RotateAround(target.transform.position, target.transform.up, Input.GetAxis("Mouse X") * rotatespeed); 
    }

    // funkcja obslugujaca zdarzenie przyblizania/oddalania kamery
    private void zoom()
    {
        // obliczenie dystancy pomiedzy kamera a punktem (centralnym punktem robota)
        float distance = Vector3.Distance(transform.position, target.transform.position);

        // utworzenie zmiennej pomocniczej, ktora dla scroll'owania w gore (do przodu) ustawia sie na 1, a w dol (do tylu) na -1
        float direction = Input.mouseScrollDelta.y > 0 ? 1 : -1;

        // zmiana pozycji kamery w czasie, uwzgledniajac kierunek, predkosc
        transform.position += direction * (transform.forward * zoomspeed * Time.deltaTime);

    }
}
