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
    private int currentGameObjectIndex = 0;
    public Transform KnifeTarget;
    public GameObject countdownText;
    public Transform LineParent;
    private float AccuracyDist;
    
    private List<Vector3> currentLinePoints = new List<Vector3>(); //Liste med alle punkterne som linjen er lavet ud af
    private LineRenderer currentLine;
    private List<LineRenderer> allLines = new List<LineRenderer>(); //Liste med alle linjer

    public CountDownTimer countDownTimer;
    public Blood Blood;

    public float GetAccuracyDist() //funktion til at hente accuracyDist der kaldes på i CountDownscipt når tiden er 0 og man ikke kan tegner mere
    {
        // Calculate the average target position
        Vector3 averageTargetPos = CalcAverageTargetPos(FishyTargetParent);

        // Calculate the line position
        Vector3 linePos = CalculateLinePos(allLines);

        // Calculate the accuracy distance
        AccuracyDist = (averageTargetPos - linePos).magnitude;

        return AccuracyDist;
        //AccuracyDist = (CalcAverageTargetPos(FishyTargetParent) - CalculateLinePos(allLines)).magnitude;
        //return AccuracyDist;
    }

    void Start()
    {

    }

    void Update()
    {
        //Kniven følger med musen
        Vector3 MousePos = Input.mousePosition; //Musens position defineres
        MousePos.z = KnivDistFraKam;
        KnifeTarget.position = CookCam.ScreenToWorldPoint(MousePos);                                                //ScreenToWorldPoint laver musens position på skærmen om til en position i "verden". Dog er positionen 2-dimensionel (x,y,?)

        //Når man trykker starter en ny linje
        if (Input.GetMouseButtonDown(0))
        {
            StartNewLine();
        }

        
        //Når knappen er nede tegner listen
        if (Input.GetMouseButton(0))
        {
            Blood.StartCoroutine();
            Vector3 mousePos = Input.mousePosition; //Musens position defineres
            mousePos.z = AfstandTilKam;                                                                                                                     // Afstanden som der tegnes fra kam, det er en selvvalgt z-koordinat da skærmen er 2 dimensionel
            Vector3 worldPos;
            worldPos = CookCam.ScreenToWorldPoint(mousePos);                                                //ScreenToWorldPoint laver musens position på skærmen om til en position i "verden". Dog er positionen 2-dimensionel (x,y,?)
            DrawPosition(worldPos);                                                                     //Tegner til ved positionen i verden (som var musens position der er blevet omdannet)
        }

        //Når man giver slip stopper linjen
        if (Input.GetMouseButtonUp(0))
        {
            FinishLine();
        }
    }

    void StartNewLine()
    {
        currentLine = Instantiate(lineRenderer, Vector3.zero, Quaternion.identity, LineParent);     // Ny linje = LavNytGameObjekt(LinerendererPrefabet bliver lavet, Objektets posistion er (0,0,0), Objektet har ingen rotation, LineParent er alle de nye objekterns parent)
        currentLine.positionCount = 0;                                                              //Sætter listen med det nuværende punkter til at være tom, altså der er ingen punkter i listen
        currentLine.endWidth = currentLine.startWidth = lineWidth;                              //Læs det 
        allLines.Add(currentLine);

        //CountdownText objektet tændes 
        countdownText.SetActive(true);

        //countdown scriptet tændes også)
        CountDownTimer countDownTimer = countdownText.GetComponent<CountDownTimer>();               //få adgang til countdown script
        countDownTimer.enabled = true;

        //Timeren startes når en linje tegnes
        countDownTimer.StartTimer();

    }

    void DrawPosition(Vector3 position)
    {
        currentLinePoints.Add(position);                                    //Tilføjer musens position til listen med punkter
        currentLine.positionCount = currentLinePoints.Count;                    //Antallet af punkter i linjen sættes lig med antallet af punkter som vi selv har defineret at linjen skal have
        currentLine.SetPosition(currentLinePoints.Count - 1, position);     //Tager et indeks og en position. index er fra 0 men de naturlige tal (som bruges når man talle rantallet af punkter i linjen) er fra 1)
    }

    void FinishLine()
    {
        currentLine = null;                     //Stopper den nuværende linje
        currentLinePoints.Clear();              //Listen med den nuværende linje tømmes
    }

    Vector3 CalculateLinePos(List<LineRenderer> AllLines) //Beskriv denne funktion. Det er en ny version af calculateLinePos. Da der gemmer nye targets så skal vi kunne assigne alle de forskellige targets så man kan loop over alt det her
    {
        // hvis der ikke er noget i listen er den gennemsnitlige vektor 0
        if (AllLines.Count == 0)
        {
            return Vector3.zero;
        }

        int numberOfPoints = 0; //Antal
        Vector3 sum = Vector3.zero; //summen sættes til 0

        foreach (LineRenderer line in AllLines) //Der itereres over hvert element i listen med punkterne
        {
            if (line != null) // tjek om line rendereren findes... da de tidligere linjer slettes fra spillet, men stadig er en del af denne liste, skal de alle ignores. De er "tomme/ikke-eksisterende" renderes
            {
                Vector3[] points = new Vector3[line.positionCount]; //Laver en array (en liste i praksis) ved navn 'point' der har samme størrelse som mængden af punkter i en given linje
                numberOfPoints = numberOfPoints + line.positionCount; //Tæller antallet af punkter
                line.GetPositions(points); //for hvert punkt finder den posistionen
                foreach (Vector3 position in points)
                {
                    sum = sum + position; //De summes op
                }
            }
        }

        return sum / numberOfPoints; //Summen divideres med antallet af punkter for at få gennemsnit
    }

    //SKRIV OM DETTE I RAPPORTEN. NEDENSTÅENDE FUNKTION BEGREREGNER AVG-TARRGET-POS VED AT BERENGE FOR ET OBJECTS CHILDREN. EFTER DU HAR BESKREVET DETTE SÅ BESKRI PROBLEMET OM AT AVG-TARGET-POS SKAL ÆNDRE SIG PÅ BAGRUND AF FISKEN SÅ DENNE FUNKTION SKAL MODIFICERES TIL AT TAGE EN LISTE SOM ARGUMENT OG BERENGE TARGET POS FOR DET NÆSTE INDEX HVER GANG DEN KALDES
    //Vector3 CalcAverageTargetPos(GameObject gameObject) //GameObject parameteren er fordi den skal tage et gameobject (og dens children) som et argument. Det andet gameObject er bare fordi der skal være et navn
    //{
    //    Transform[] children = FishyTargetParent.GetComponentsInChildren<Transform>(); //Laver en array (en liste i praksis) med alle transforms fra objektets children
    //    Vector3 sumPos = Vector3.zero; // Summen af positionen af alle børnene sættes til 0


    //    foreach (Transform child in children)
    //    {
    //        sumPos = sumPos + child.position; //De summes op
    //    }

    //    return sumPos / children.Length; //Gennemsnittet beregnes (summen divideret med antallet af children)
    //}

    Vector3 CalcAverageTargetPos(List<GameObject> gameObjectList)
    {
        //Tjekker om listen er tom, hvis ja så er den gennemsnitlige pos 0
        if (gameObjectList.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 sumPos = Vector3.zero; // Summen af positionen af alle børnene sættes til 0
        int totalChildren = 0; // antalet af børn objektet har sættes til 0

        //Henter det object der skal berenges på ved brug af index
        GameObject currentGameObject = gameObjectList[currentGameObjectIndex];

        //tjekker om det nuværende object findes
        if (currentGameObject != null)
        {
            //Laver en array (en liste i praksis) med alle transforms fra objektets children
            Transform[] children = currentGameObject.GetComponentsInChildren<Transform>();

            foreach (Transform child in children)
            {
                sumPos = sumPos + child.position; //De summes op
                totalChildren = totalChildren + 1; //tæller antallet af børn (dette skal bruges til at beregne gennemsnit.. gennemsnit = sum/antal)
            }
        }

        //Hvis der er ingen børn bliver der ik divideret med 0, i stedet returneres en 0 vektor
        if (totalChildren == 0)
        {
            return Vector3.zero;
        }

        //beregn gennemsnit
        Vector3 averagePosition = sumPos / totalChildren;

        //går en op i index. Der tages modulo til antallet af gameobjekter så at hvis der er 3 objekter og tælleren er noget til 4 looper den tilbage til 1
        currentGameObjectIndex = (currentGameObjectIndex + 1) % gameObjectList.Count;

        //Returnere gennemsnitlige pos
        return averagePosition;
    }


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



public void DisableAllLineRenderer()
    {
        foreach (LineRenderer line in allLines)
        {
            if (line != null) // tjek om line rendereren findes... da de tidligere linjer slettes fra spillet, men stadig er en del af denne liste, skal de alle ignores. De er "tomme/ikke-eksisterende" renderes
            {
                line.enabled = false; //Slukker line renderer da en line renderer hedder line (tjek loopens argument)
            }
        }
    }


    public void EnableAllLineRenderer()
    {
        foreach (LineRenderer line in allLines)
        {
            if (line != null) // tjek om line rendereren findes... da de tidligere linjer slettes fra spillet, men stadig er en del af denne liste, skal de alle ignores. De er "tomme/ikke-eksisterende" renderes
            {
                line.enabled = true; //Tænder line renderer da en line renderer hedder line (tjek loopens argument)
            }
        }
    }
}
