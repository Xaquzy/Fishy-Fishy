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
    public Transform CutSceneDir;
    public CinemachineFreeLook MainCam;
    public GameObject CutSceneCam;
    public GameObject CookCam;
    
    //Animation
    public Animator PlayerAnimator;

    //public GameObject WinCam;
    public GameObject CutSceneTestText;
    public float CutSceneTime = 5f;
    public float RatingReadTime = 5f;
    public GameObject knife; //S� den kan slukkes i cutscene
    public GameObject hose; //S� den kan slukkes i cutscene
    public GameObject hand; //S� den kan slukkes i cutscene
    public GameObject CsKnife; //S� den kan t�ndes i cutscene
    public LineRenderer lineRenderer;
    public GameObject CountdownTimerText; //S� den kan slukkes i cutscene
    public CookZone CookZone;
    public Drawing Drawing;
    public RatingManager RatingManager;
    public DropObjZone DropObjZone;
    private int AntalCutScenesSpillet = 0;
    public int AntalCutScenesISpillet = 10;

    // Update is called once per frame
    void Update()
    {
        if (AntalCutScenesSpillet == AntalCutScenesISpillet)
        {
            Debug.Log("FINAL CUTSCENE");
            StartCoroutine(FinalCutscene());
        }
    }

    public void StartCutScene()
    {
        StartCoroutine(CutSceneInScene());
    }

    public IEnumerator CutSceneInScene()
    {
        //Slukker for movement script
        Movement movement = Player.GetComponent<Movement>();
        movement.enabled = false;

        //Slukker for alle egenskaber
        CookZone.SlukEgenskaber();

        //Placer spilleren i det rigtige sted og med korrekt rotation
        Player.position = CutScenePos.position;
        Player.LookAt(CutSceneDir);

        //objekterne skal slukkes (CS kniv t�ndes)
        hose.SetActive(false);
        hand.SetActive(false);
        CsKnife.SetActive(true);

        //g�r linjerne gennemsigitg. Hvis linjerne bare slettes s� er der ikke en position der kan bruges til at bestemme den rating man skal f� senere i rating()
        Drawing.DisableAllLineRenderer();

        //Camera ting
        CookCam.SetActive(false);
        CutSceneCam.SetActive(true);
        MainCam.enabled = false;

        //Den reelle cutscene
        //Afspil animationer
        //Afspil Lyd
        CutSceneTestText.SetActive(true); //Det er bare en placeholder tester
        PlayerAnimator.SetBool("Cutscene", true);

        yield return new WaitForSeconds(CutSceneTime); //kode til lidt pause s� man kan se Cutscene f�r ratingen popper up
        RatingManager.Rating();

        //Linjerne skal slettes efter de er blevet brugt til at bestemme en rating
        Drawing.DeleteAllLines();
        Drawing.EnableAllLineRenderer(); //man kan se linjer til n�ste gang
        yield return new WaitForSeconds(RatingReadTime); //Lidt tid til at l�se sin rating

        //G�r klar til fortsat spil
        CountdownTimerText.SetActive(false); //Timer texten slukkes
        MainCam.enabled = true;
        CutSceneCam.SetActive(false);

        CutSceneTestText.SetActive(false); //Det er bare en placeholder tester
        PlayerAnimator.SetBool("Cutscene", false);

        RatingManager.TheRating.SetActive(false); //Sletter ratingen p� sk�rmen s� man kan spille videre
        DropObjZone.AmountToMoveOnIndex = (DropObjZone.AmountToMoveOnIndex + 1) % DropObjZone.AmountToMoveOn.Count;  //Opdaterer ens index til zonescore ting til organdr                                                                                                          
        DropObjZone.ZoneScore = DropObjZone.AmountToMoveOn[DropObjZone.AmountToMoveOnIndex];

        CsKnife.SetActive(false); //Kniv slukkes

        //Reset spillerens position i k�kkenet?

        //T�nder for movement script og slukker for ratingen (og for Cutscenetext som er placeholder)
        movement.enabled = true; //.SetActive(false);
        AntalCutScenesSpillet = AntalCutScenesSpillet + 1;
    }

    public IEnumerator FinalCutscene()
    {
        //SKIFT TIL FINAL WIN CAM
        //WinCam.SetActive(true);
        //CookCam.SetActive(false);
        //CutSceneCam.SetActive(false);
        //MainCam.enabled = false;

        yield return new WaitForSeconds(18f);

        RatingManager.DisplayRatings();
    }
}
