using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class CutScene : MonoBehaviour
{
    //Cutscene ting
    public Transform Player;
    public Transform CutScenePos;
    public Transform PosCutscenePos;
    public Transform CutSceneDir;
    public CinemachineFreeLook MainCam;
    public GameObject CutSceneCam;
    public GameObject CookCam;
    public SwapObjects SwapObjects;
    public List<GameObject> NonFinalRatingCanvas = new List<GameObject>();
    public GameObject HoldETekst;
    public GameObject TrykToolTip;

    //Animation
    public Animator PlayerAnimator;

    public GameObject WinCam;
    public float CutSceneTime = 5f;
    public float RatingReadTime = 5f;
    public GameObject knife; //Så den kan slukkes i cutscene
    public GameObject hose; //Så den kan slukkes i cutscene
    public GameObject hand; //Så den kan slukkes i cutscene
    public GameObject CsKnife; //Så den kan tændes i cutscene
    public LineRenderer lineRenderer;
    public GameObject CountdownTimerText; //Så den kan slukkes i cutscene
    public CookZone CookZone;
    public TutCookZone TutCookZone;
    public Drawing Drawing;
    public RatingManager RatingManager;
    public DropObjZone DropObjZone;
    public CountDownTimer CountDownTimer;
    public int AntalCutScenesISpillet = 10;
    private int AntalCutScenesSpillet = 0;

    // Update is called once per frame
    void Update()
    {

    }

    public void StartCutScene()
    {
        StartCoroutine(CutSceneInScene());
    }

    public IEnumerator CutSceneInScene()
    {
        CookZone.InCookMode = false;
        //Slukker for movement script
        Movement movement = Player.GetComponent<Movement>();
        movement.enabled = false;

        //Slukker for alle egenskaber
        if (CookZone == null)
        {
            TutCookZone.SlukEgenskaber();
        }
        else
        {
            CookZone.SlukEgenskaber();
        }


        //Placer spilleren i det rigtige sted og med korrekt rotation
        Player.position = CutScenePos.position;
        Player.LookAt(CutSceneDir);

        //objekter og ui skal slukkes (CS kniv tændes)
        hose.SetActive(false);
        hand.SetActive(false);
        CsKnife.SetActive(true);
        HoldETekst.SetActive(false);

        //gør linjerne gennemsigitg. Hvis linjerne bare slettes så er der ikke en position der kan bruges til at bestemme den rating man skal få senere i rating()
        Drawing.DisableAllLineRenderer();

        //Camera ting
        CookCam.SetActive(false);
        CutSceneCam.SetActive(true);
        MainCam.enabled = false;

        //Den reelle cutscene
        //Afspil animationer
        //Afspil Lyd
        PlayerAnimator.SetBool("Cutscene", true);

        yield return new WaitForSeconds(CutSceneTime); //kode til lidt pause så man kan se Cutscene før ratingen popper up
        RatingManager.Rating();

        //Linjerne skal slettes efter de er blevet brugt til at bestemme en rating
        Drawing.DeleteAllLines();
        Drawing.EnableAllLineRenderer(); //man kan se linjer til næste gang
        yield return new WaitForSeconds(RatingReadTime); //Lidt tid til at læse sin rating

        //Gør klar til fortsat spil
        CountdownTimerText.SetActive(false); //Timer texten slukkes
        MainCam.enabled = true;
        CutSceneCam.SetActive(false);

        PlayerAnimator.SetBool("Cutscene", false);
        SwapObjects.SwapObjectsInList();

        RatingManager.TheRating.SetActive(false); //Sletter ratingen på skærmen så man kan spille videre
        DropObjZone.AmountToMoveOnIndex = (DropObjZone.AmountToMoveOnIndex + 1) % DropObjZone.AmountToMoveOn.Count;  //Opdaterer ens index til zonescore ting til organdr                                                                                                          
        DropObjZone.ZoneScore = DropObjZone.AmountToMoveOn[DropObjZone.AmountToMoveOnIndex];

        CsKnife.SetActive(false); //Kniv slukkes
        TrykToolTip.SetActive(false);

        //Reset spillerens position i køkkenet
        Player.position = PosCutscenePos.position;

        //Tænder for movement script og slukker for ratingen (og for Cutscenetext som er placeholder)
        movement.enabled = true; //.SetActive(false);
        AntalCutScenesSpillet = AntalCutScenesSpillet + 1;
    }

    public IEnumerator FinalCutscene()
    {
        //Skift til final cam og sluk alt
        if (WinCam != null)
        {
            WinCam.SetActive(true);
        }
        CookCam.SetActive(false);
        CutSceneCam.SetActive(false);
        MainCam.enabled = false;
        //Sluk alt i canvas ud over final rating
        for (int i = 0; i < NonFinalRatingCanvas.Count; i++)
        {
            NonFinalRatingCanvas[i].SetActive(false);
        }
        
        RatingManager.DisplayRatings();
        yield return new WaitForSeconds(3f);

    }
}
