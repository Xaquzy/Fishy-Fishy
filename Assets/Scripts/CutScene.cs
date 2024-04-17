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
    public CinemachineFreeLook MainCam;
    public GameObject CutSceneCam;
    public GameObject CookCam;
    public GameObject WinCam;
    public GameObject CutSceneTestText;
    public float CutSceneTime = 5f;
    public float RatingReadTime = 5f;
    public GameObject knife; //Så den kan slukkes i cutscene
    public GameObject hose; //Så den kan slukkes i cutscene
    public GameObject hand; //Så den kan slukkes i cutscene
    public LineRenderer lineRenderer;
    public GameObject CountdownTimerText; //Så den kan slukkes i cutscene
    public CookZone CookZone;
    public Drawing Drawing;
    public RatingManager RatingManager;
    public DropObjZone DropObjZone;
    private int AntalCutScenesSpillet = 0;
    public int AntalCutScenesISpillet = 11;

    // Update is called once per frame
    void Update()
    {
        if (AntalCutScenesSpillet == AntalCutScenesISpillet)
        {
            //SKIFT TIL FINAL WIN CAM

            RatingManager.DisplayRatings();
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

        //Placer spilleren i det rigtige sted 
        Player.position = CutScenePos.position;

        //objekterne skal slukkes
        knife.SetActive(false);
        hose.SetActive(false);
        hand.SetActive(false);

        //gør linjerne gennemsigitg. Hvis linjerne bare slettes så er der ikke en position der kan bruges til at bestemme den rating man skal få senere i rating()
        Drawing.DisableAllLineRenderer();

        //Camera ting
        CookCam.SetActive(false);
        CutSceneCam.SetActive(true);
        MainCam.enabled = false;

        //Den reelle cutscene
        //Afspil animationer
        //Afspil Lyd
        CutSceneTestText.SetActive(true); //Det er bare en placeholder tester

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

        CutSceneTestText.SetActive(false); //Det er bare en placeholder tester

        RatingManager.TheRating.SetActive(false); //Sletter ratingen på skærmen så man kan spille videre
        DropObjZone.AmountToMoveOnIndex = (DropObjZone.AmountToMoveOnIndex + 1) % DropObjZone.AmountToMoveOn.Count;  //Opdaterer ens index til zonescore ting til organdr                                                                                                          
        DropObjZone.ZoneScore = DropObjZone.AmountToMoveOn[DropObjZone.AmountToMoveOnIndex];

        knife.SetActive(true); //Kniven skal findes igen

        //Reset spillerens position i køkkenet?

        //Tænder for movement script og slukker for ratingen (og for Cutscenetext som er placeholder)
        movement.enabled = true; //.SetActive(false);
        AntalCutScenesSpillet = AntalCutScenesSpillet + 1;
    }
}
