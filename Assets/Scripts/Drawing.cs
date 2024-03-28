using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Drawing : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Camera CookCam;
    public float lineWidth = 0.1f;
    public float AfstandTilKam = 8f;
    public float KnivDistFraKam = 1.5f;
    public List<GameObject> FishyTargetParent = new List<GameObject>();
    public Transform KnifeTarget;
    public GameObject countdownText;
    public Transform LineParent;
    private float AccuracyDist;

    private LineRenderer currentLine;
    private List<Vector3> currentLinePoints = new List<Vector3>(); //Liste med alle punkterne som linjen er lavet ud af
    private List<LineRenderer> allLines = new List<LineRenderer>(); //Liste med alle linjer

    public CountDownTimer countDownTimer;
    public float GetAccuracyDist() //funktion til at hente accuracyDist der kaldes p� i CountDownscipt n�r tiden er 0 og man ikke kan tegner mere
    {
        AccuracyDist = (CalcAverageTargetPos(FishyTargetParent) - CalculateLinePos(allLines)).magnitude;
        return AccuracyDist;
    }

    void Start()
    {

    }

    void Update()
    {
        //Kniven f�lger med musen
        Vector3 MousePos = Input.mousePosition; //Musens position defineres
        MousePos.z = KnivDistFraKam;
        KnifeTarget.position = CookCam.ScreenToWorldPoint(MousePos); //ScreenToWorldPoint laver musens position p� sk�rmen om til en position i "verden". Dog er positionen 2-dimensionel (x,y,?)

        //N�r man trykker starter en ny linje
        if (Input.GetMouseButtonDown(0))
        {
            StartNewLine();
        }

        Vector3 worldPos;
        //N�r knappen er nede tegner listen
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition; //Musens position defineres
            mousePos.z = AfstandTilKam; // Afstanden som der tegnes fra kam, det er en selvvalgt z-koordinat da sk�rmen er 2 dimensionel
            worldPos = CookCam.ScreenToWorldPoint(mousePos); //ScreenToWorldPoint laver musens position p� sk�rmen om til en position i "verden". Dog er positionen 2-dimensionel (x,y,?)
            DrawPosition(worldPos); //Tegner til ved positionen i verden (som var musens position der er blevet omdannet)
        }

        //N�r man giver slip stopper linjen
        if (Input.GetMouseButtonUp(0))
        {
            FinishLine();
        }
    }

    void StartNewLine()
    {
        currentLine = Instantiate(lineRenderer, Vector3.zero, Quaternion.identity, LineParent); // Ny linje = LavNytGameObjekt(LinerendererPrefabet bliver lavet, Objektets posistion er (0,0,0), Objektet har ingen rotation, LineParent er alle de nye objekterns parent)
        currentLine.positionCount = 0; //S�tter listen med det nuv�rende punkter til at v�re tom, alts� der er ingen punkter i listen
        currentLine.startWidth = lineWidth; //L�s det 
        currentLine.endWidth = lineWidth; //L�s det 
        allLines.Add(currentLine);

        //CountdownText objektet t�ndes 
        countdownText.SetActive(true);

        //countdown scriptet t�ndes ogs�)
        CountDownTimer countDownTimer = countdownText.GetComponent<CountDownTimer>(); //f� adgang til countdown script
        countDownTimer.enabled = true;

        //Timeren startes n�r en linje tegnes
        countDownTimer.StartTimer();

    }

    void DrawPosition(Vector3 position)
    {
        currentLinePoints.Add(position); //Tilf�jer musens position til listen med punkter
        currentLine.positionCount = currentLinePoints.Count; //Antallet af punkter i linjen s�ttes lig med antallet af punkter som vi selv har defineret at linjen skal have
        currentLine.SetPosition(currentLinePoints.Count - 1, position); //Tegner en linje fra det forrige punkt til det nuv�rende punkt
    }

    void FinishLine()
    {
        currentLine = null; //Stopper den nuv�rende linje
        currentLinePoints.Clear(); //Listen med den nuv�rende linje t�mmes
    }

    Vector3 CalculateLinePos(List<LineRenderer> listWithLinePoints) //Beskriv denne funktion. Det er en ny version af calculateLinePos. Da der gemmer nye targets s� skal vi kunne assigne alle de forskellige targets s� man kan loop over alt det her
    {
        // hvis der ikke er nogle vektorer i listen er den gennemsnitlige vektor 0
        if (listWithLinePoints.Count == 0)
        {
            return Vector3.zero;
        }

        int numberOfPoints = 0; //Antal
        Vector3 sum = Vector3.zero; //summen s�ttes til 0

        foreach (LineRenderer line in listWithLinePoints) //Der itereres over hvert element i listen med punkterne
        {
            if (line != null) // tjek om line rendereren findes... da de tidligere linjer slettes fra spillet, men stadig er en del af denne liste, skal de alle ignores. De er "tomme/ikke-eksisterende" renderes
            {
                Vector3[] points = new Vector3[line.positionCount]; //Laver en array (en liste i praksis) ved navn 'point' der har samme st�rrelse som m�ngden af punkter i en given linje
                numberOfPoints = numberOfPoints + line.positionCount; //T�ller antallet af punkter
                line.GetPositions(points); //for hvert punkt finder den posistionen
                foreach (Vector3 position in points)
                {
                    sum = sum + position; //De summes op
                }
            }
        }

        return sum / numberOfPoints; //Summen divideres med antallet af punkter for at f� gennemsnit
    }

    Vector3 CalcAverageTargetPos(List<GameObject> gameObjectList)
    {
        Vector3 sumPos = Vector3.zero; // Sum of positions of all children
        int totalChildren = 0; // Total number of children

        foreach (GameObject gameObject in gameObjectList)
        {
            if (gameObject != null)
            {
                Transform[] children = gameObject.GetComponentsInChildren<Transform>(); // Get all child transforms

                // Exclude the parent's own transform if needed
                // (uncomment the line below if the parent's position should not be included in the average)
                // sumPos -= gameObject.transform.position;

                foreach (Transform child in children)
                {
                    sumPos += child.position; // Add child position to the sum
                    totalChildren++; // Increment total children count
                }
            }
        }

        // Avoid division by zero
        if (totalChildren == 0)
        {
            return Vector3.zero;
        }

        // Calculate average position
        return sumPos / totalChildren;
    }

    //Vector3 CalcAverageTargetPos(GameObject gameObject) //GameObject parameteren er fordi den skal tage et gameobject (og dens children) som et argument. Det andet gameObject er bare fordi der skal v�re et navn
    //{
    //    Transform[] children = FishyTargetParent.GetComponentsInChildren<Transform>(); //Laver en array (en liste i praksis) med alle transforms fra objektets children
    //    Vector3 sumPos = Vector3.zero; // Summen af positionen af alle b�rnene s�ttes til 0


    //    foreach (Transform child in children)
    //    {
    //        sumPos = sumPos + child.position; //De summes op
    //    }

    //    return sumPos / children.Length; //Gennemsnittet beregnes (summen divideret med antallet af children)
    //}
    public void DeleteAllLines()
    {
        foreach (LineRenderer line in allLines)
        {
            if (line != null) // tjek om line rendereren findes... da de tidligere linjer slettes fra spillet, men stadig er en del af denne liste, skal de alle ignores. De er "tomme/ikke-eksisterende" renderes
            {
                Destroy(line.gameObject); //Sletter alle objekterne i listen med linjerne
            }
        }
    }



public void SlukRendererForAlleLinjer()
    {
        foreach (LineRenderer line in allLines)
        {
            if (line != null) // tjek om line rendereren findes... da de tidligere linjer slettes fra spillet, men stadig er en del af denne liste, skal de alle ignores. De er "tomme/ikke-eksisterende" renderes
            {
                line.enabled = false; //Slukker line renderer da en line renderer hedder line (tjek loopens argument)
            }
        }
    }


    public void T�ndRendererForAlleLinjer()
    {
        foreach (LineRenderer line in allLines)
        {
            if (line != null) // tjek om line rendereren findes... da de tidligere linjer slettes fra spillet, men stadig er en del af denne liste, skal de alle ignores. De er "tomme/ikke-eksisterende" renderes
            {
                line.enabled = true; //T�nder line renderer da en line renderer hedder line (tjek loopens argument)
            }
        }
    }
}
