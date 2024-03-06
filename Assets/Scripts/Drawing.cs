using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Camera mainCamera;
    public Transform LineParent;
    public float lineWidth = 0.1f;
    public float AfstandTilKam = 8f;

    private LineRenderer currentLine;
    private List<Vector3> currentLinePoints = new List<Vector3>(); //Liste med alle punkterne som linjen er lavet ud af
    private List<LineRenderer> allLines = new List<LineRenderer>(); //Liste med alle linjer

    void Update()
    {
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
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos); //ScreenToWorldPoint laver musens position på skærmen om til en position i "verden". Dog er positionen 2-dimensionel (x,y,?)
            DrawPosition(worldPos); //Tegner til ved positionen i verden (som var musens position der er blevet omdannet)
        }

        //Når man giver slip stopper linjen
        if (Input.GetMouseButtonUp(0))
        {
            FinishLine();
        }

        Vector3 averageVector = CalculateAverage(allLines);
        Debug.Log("Average Vector: " + averageVector);
    }

    void StartNewLine()
    {
        currentLine = Instantiate(lineRenderer, Vector3.zero, Quaternion.identity, LineParent); // Ny linje = LavNytGameObjekt(LinerendererPrefabet bliver lavet, Objektets posistion er (0,0,0), Objektet har ingen rotation, LineParent er alle de nye objekterns parent)
        currentLine.positionCount = 0; //Sætter listen med det nuværende punkter til at være tom, altså der er ingen punkter i listen
        currentLine.startWidth = lineWidth; //Læs det 
        currentLine.endWidth = lineWidth; //Læs det 
        allLines.Add(currentLine);
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

    Vector3 CalculateAverage(List<LineRenderer> listWithLinePoints)
    {
        // hvis der ikke er nogle vektorer i listen er den gennemsnitlige vektor 0
        if (listWithLinePoints.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 sum = Vector3.zero; //summen sættes til 0
        foreach (LineRenderer line in listWithLinePoints) //Der itereres over hvert element i listen med punkterne
        {
            Vector3[] points = new Vector3[line.positionCount]; //Laver en array (en liste i praksis) ved navn point der har samme størrelse som mængden af punkter i en given linje
            line.GetPositions(points); //for hvert punkt finder den posistionen
            foreach (Vector3 position in points)
            {
                sum = sum + position; //De summes op
            }
        }

        return sum / listWithLinePoints.Count; //Summen divideres med antallet af punkter for at få gennemsnit
    }
}
