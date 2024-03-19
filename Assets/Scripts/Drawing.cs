using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Camera CookCam;
    public float lineWidth = 0.1f;
    public float AfstandTilKam = 8f;
    public float KnivDistFraKam = 1.5f;
    public GameObject FishyTargetParent;
    public Transform KnifeTarget;
    public GameObject countdownText;
    private float AccuracyDist;

    private LineRenderer currentLine;
    private List<Vector3> currentLinePoints = new List<Vector3>(); //Liste med alle punkterne som linjen er lavet ud af
    private List<LineRenderer> allLines = new List<LineRenderer>(); //Liste med alle linjer

    public float GetAccuracyDist() //funktion til at hente accuracyDist der kaldes på i CountDownscipt når tiden er 0 og man ikke kan tegner mere
    {
        AccuracyDist = (CalcAverageTargetPos(FishyTargetParent) - CalculateLinePos(allLines)).magnitude;
        return AccuracyDist;
    }

    void Start()
    {
        //DETTE ER KUN FOR AT DEBUGGE SLET SNAREST: Beregner avg fishtarget ved at kalde på funktionen 
        Vector3 averageFishTarget = CalcAverageTargetPos(FishyTargetParent);
        Debug.Log("Average TargetPos: " + averageFishTarget);
    }

    void Update()
    {
        //Kniven følger med musen
        Vector3 MousePos = Input.mousePosition; //Musens position defineres
        MousePos.z = KnivDistFraKam;
        KnifeTarget.position = CookCam.ScreenToWorldPoint(MousePos); //ScreenToWorldPoint laver musens position på skærmen om til en position i "verden". Dog er positionen 2-dimensionel (x,y,?)

        //Når man trykker starter en ny linje
        if (Input.GetMouseButtonDown(0))
        {
            StartNewLine();
        }

        //Når knappen er nede tegner listen
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition; //Musens position defineres
            mousePos.z = AfstandTilKam; // Afstanden som der tegnes fra kam, det er en selvvalgt z-koordinat da skærmen er 2 dimensionel
            Vector3 worldPos = CookCam.ScreenToWorldPoint(mousePos); //ScreenToWorldPoint laver musens position på skærmen om til en position i "verden". Dog er positionen 2-dimensionel (x,y,?)
            DrawPosition(worldPos); //Tegner til ved positionen i verden (som var musens position der er blevet omdannet)
        }

        //Når man giver slip stopper linjen
        if (Input.GetMouseButtonUp(0))
        {
            FinishLine();
        }

        Vector3 averageLinePos = CalculateLinePos(allLines);
        Debug.Log("Average LinePos: " + averageLinePos);

        //Beregn distancen mellem avgfishyTargets og linjen...magnitude konverterer det til en længde
        Vector3 averageFishTarget = CalcAverageTargetPos(FishyTargetParent);
        Debug.Log("Average TargetPos: " + averageFishTarget);

        float AccuracyDist = (averageFishTarget - averageLinePos).magnitude;
        Debug.Log("Accuracy Score: " + AccuracyDist);
    }

    void StartNewLine()
    {
        currentLine = Instantiate(lineRenderer, Vector3.zero, Quaternion.identity, transform); // Ny linje = LavNytGameObjekt(LinerendererPrefabet bliver lavet, Objektets posistion er (0,0,0), Objektet har ingen rotation, LineParent er alle de nye objekterns parent)
        currentLine.positionCount = 0; //Sætter listen med det nuværende punkter til at være tom, altså der er ingen punkter i listen
        currentLine.startWidth = lineWidth; //Læs det 
        currentLine.endWidth = lineWidth; //Læs det 
        allLines.Add(currentLine);

        //CountdownText objektet tændes (derved tændes countdown scriptet på det også)
        countdownText.SetActive(true);
    }

    void DrawPosition(Vector3 position)
    {
        currentLinePoints.Add(position); //Tilføjer musens position til listen med punkter
        currentLine.positionCount = currentLinePoints.Count; //Antallet af punkter i linjen sættes lig med antallet af punkter som vi selv har defineret at linjen skal have
        currentLine.SetPosition(currentLinePoints.Count - 1, position); //Tegner en linje fra det forrige punkt til det nuværende punkt
    }

    void FinishLine()
    {
        currentLine = null; //Stopper den nuværende linje
        currentLinePoints.Clear(); //Listen med den nuværende linje tømmes
    }

    Vector3 CalculateLinePos(List<LineRenderer> listWithLinePoints)
    {
        // hvis der ikke er nogle vektorer i listen er den gennemsnitlige vektor 0
        if (listWithLinePoints.Count == 0)
        {
            return Vector3.zero;
        }

        int numberOfPoints = 0; //Antal
        Vector3 sum = Vector3.zero; //summen sættes til 0
        foreach (LineRenderer line in listWithLinePoints) //Der itereres over hvert element i listen med punkterne
        {
            Vector3[] points = new Vector3[line.positionCount]; //Laver en array (en liste i praksis) ved navn 'point' der har samme størrelse som mængden af punkter i en given linje
            numberOfPoints = numberOfPoints + line.positionCount; //Tæller antallet af punkter
            line.GetPositions(points); //for hvert punkt finder den posistionen
            foreach (Vector3 position in points)
            {
                sum = sum + position; //De summes op
            }
        }
        return sum / numberOfPoints; //Summen divideres med antallet af punkter for at få gennemsnit
    }

    Vector3 CalcAverageTargetPos(GameObject gameObject) //GameObject parameteren er fordi den skal tage et gameobject (og dens children) som et argument. Det andet gameObject er bare fordi der skal være et navn
    {
        Transform[] children = FishyTargetParent.GetComponentsInChildren<Transform>(); //Laver en array (en liste i praksis) med alle transforms fra objektets children
        Vector3 sumPos = Vector3.zero; // Summen af positionen af alle børnene sættes til 0


        foreach (Transform child in children)
        {
            sumPos = sumPos + child.position; //De summes op
        }

        return sumPos / children.Length; //Gennemsnittet beregnes (summen divideret med antallet af children)
    }

}
