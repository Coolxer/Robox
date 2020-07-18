using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Klasa reprezentujaca kinematyke robota (automatyczny ruch)
// Podstawowym pojeciem jest forward kinematics (kinematyka prosta)
// Polega to na tym, ze poruszajac jednym wezlem inne rowniez sie poruszaja
// Mozna powiedziec ze dziedzicza one delte pozycji i kata, a zatem 
// zmieniaja sie wraz z ruchem "rodzica"
// Unity ulatwia uzycie tej kinematyki poprzez mechanizm zwany "parenting",
// czyli podzial na elementy nadrzedne i podrzedne (rodzic i dziecko)

// Inverse kinematics, czyli kinematyka odwrotna jest bardziej skomplikowana
// i polega na odtworzeniu naturalnych ruchow poszczegolnych stawow
// w zaleznosci od pozycji celu
// wymaga to zastosowania odpowiednich rownan
// Pokazane przeze mnie rozwiazanie nie jest idealne
// Zostalo zaczerpniete z https://www.alanzucconi.com/2017/04/10/robotic-arms/
// i nieco przerobione. Nie czerpie zadnych korzysci z uzycia tego rozwiazania
// Sluzy one jedynie edukacji i pokazaniu mozliwosci Unity
public class Kinematics : MonoBehaviour
{
    // tablica wezlow
    public Axis[] joints;

    // obiekt bedacy celem (kontrolerem robota)
    public GameObject target;

    // zmienna definiujaca tempo w jakim wartosci katow daza do optymalnego rozwiazania
    // im wieksza wartosc tym szybciej, ale tez wieksze ryzyko bledow i tzw. glitch'y
    public float learningRate = 1.0f;

    // zmienna definiujaca skok wartosci kata
    public float samplingDistance = 5.0f;

    // zmienna definiujaca prog dystansowy, czyli minimalna odleglosc miedzy celem a wezlem
    // aby zostalo podjete jakies dzialanie
    public float distanceThreshold = 0.01f;

    // wektor reprezentujacy ostatnia pozycje celu
    private Vector3 lastPos;

    // funkcja uruchamiana tylko raz podczas tworzenia obiektu (ala constructor)
    void Start()
    {
        // zapisanie aktualnej pozycji celu jako ostatniej zapamietanej
        lastPos = target.transform.position;
    } 

    // funkcja "main" dla klasy Kinematics, uruchamiana w nieskonczonej petli
    void Update()
    {   
        // sprawdzenie czy cel (kontroler) zmienil pozycje
        // jesli nie to nie ma sensu przeliczac wezlow
        if(lastPos != target.transform.position)
            // obliczanie i transformacja wezlow
            InverseKinematics();
    }

    // funkcja rotujaca wzgledem zadanej osi i zwracajaca nowy punkt po transformacji
    public Vector3 ForwardKinematics ()
    {
        // zapis punktu opisujacego pozycje 1'go wezla
        Vector3 prevPoint = joints[0].transform.position;

        // pobranie quateternionu identity, ktory odpowiada za brak ruchu
        // co oznacza ze obiekt jest idealnie dopasowany do osi swiata lub macierzystej
        Quaternion rotation = Quaternion.identity;

        for (int i = 1; i < joints.Length; i++)
        {
            // obrot wzledem osi
            rotation *= Quaternion.AngleAxis(joints[i - 1].getAngle(), joints[i - 1].vector);
            
            // aktualizacja punktu
            prevPoint += rotation * joints[i].startOffset;
        }

        // zwrocenie nowego punktu po transformacji
        return prevPoint;
    }

    // funkcja obliczajaca odleglosc miedzy celem a punktem obliczonym przy uzyciu funkcji forwardKinematics
    public float DistanceFromTarget()
    {
        Vector3 point = ForwardKinematics();
        return Vector3.Distance(point, target.transform.position);
    }

    // funkcja obliczajaca gradient dla wezla
    // opiera sie na rownaniach matematycznych, ktore mozna
    // znalezc w internecie badz podlinkowanym poradniku
    public float PartialGradient (int i)
    {
        // zapisanie wartosci kata
        float angle = joints[i].getAngle();
    
        // [F(x + SamplingDistance) - F(x)] / h
        float f_x = DistanceFromTarget();
    
        // zaktualizowanie wartosci kata o wartosci samplingDistance
        joints[i].setAngle(joints[i].getAngle() + samplingDistance);

        float f_x_plus_d = DistanceFromTarget();
    
        float gradient = (f_x_plus_d - f_x) / samplingDistance;
    
        joints[i].setAngle(angle);
    
        // zwrocenie obliczonej wartosci
        return gradient;
    }

    // funkcja obslugujaca kinematyke odwrotna
    public void InverseKinematics ()
    {
        // jeśli dystans celu od węzła jest mniejszy niż zdefiniowany to wyjdz z funkcji i nie podejmuj zadnych krokow)
        if (DistanceFromTarget() < distanceThreshold)
            return;
    
        for (int i = joints.Length - 1; i >= 0; i--)
        {
            // obliczenie gradientu
            float gradient = PartialGradient(i);
   
            // ustawianie katow poszczegolnych wezlow, uwzgledniajac minimalne i maksymalne obroty
            //Joints[i].setAngle(Mathf.Clamp(Joints[i].getAngle() - (LearningRate * gradient), Joints[i].min, Joints[i].max));

            // ustawianie katow poszczegolnych wezlow
            joints[i].setAngle(joints[i].getAngle() - (learningRate * gradient));
    
            // jeśli dystans celu od węzła jest mniejszy niż zdefiniowany to wyjdz z funkcji i nie podejmuj zadnych krokow)
            // jest to dodatkowe sprawdzenie przy kazdym wezle, aby nie wykonywac niepotrzebnych ruchow
            if (DistanceFromTarget() < distanceThreshold)
                return;
        }

        // zaktualizowanie ostatniej znanej pozycji kontrolera
        lastPos = target.transform.position;
    }
}
