using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class RatingManager : MonoBehaviour
{
    //Rating ting
    public GameObject ratingParent;
    [HideInInspector] public GameObject TheRating; // Et GameObject der starter tomt men senere sættes den til at være ratingen. Derefter kan ratingen kaldes i udenfor RatingManager og slukkes i CutScene 
    public Drawing Drawing;
    public DropObjZone DropObjZone;
    public Transform textParent;
    public float vertikaleMellemrum = 50f;

    void Update()
    {

    }
    //Funktion til at bestemme rating
    public void Rating()
    {
        Debug.Log("Vi er inde i rating funktionen.");
        float accuracyDist = Drawing.GetAccuracyDist(); //Få adgang til accuracyDist
        ratingParent.SetActive(true);

        //Deaktiver alle rating beskedeer
        for (int i = 0; i < ratingParent.transform.childCount; i++)
        {
            Transform t = ratingParent.transform.GetChild(i);
            t.gameObject.SetActive(false);
        }
        GameObject rating = null;

        //Ratings for drab/drop
        if (float.IsNaN(accuracyDist)) //Hvis accucacy ikke er et tal (NaN). Vi bestemte dette ved at debug.log for at finde ud af hvad vi skal hae i if-sætningen ud fra accacydist. vi startede med if accuracyDist = null
        {

            if (DropObjZone.ZoneScore >= 5)
            {
                rating = ratingParent.transform.Find("F").gameObject;
            }
            if (DropObjZone.ZoneScore == 4)
            {
                rating = ratingParent.transform.Find("D").gameObject;
            }
            if (DropObjZone.ZoneScore == 3)
            {
                rating = ratingParent.transform.Find("C").gameObject;
            }
            if (DropObjZone.ZoneScore == 2)
            {
                rating = ratingParent.transform.Find("B").gameObject;
            }
            if (DropObjZone.ZoneScore == 1)
            {
                rating = ratingParent.transform.Find("A").gameObject;
            }
            if (DropObjZone.ZoneScore == 0)
            {
                rating = ratingParent.transform.Find("S").gameObject;
            }
        }

        //Ratings for tegne
        else
        {
            Debug.Log("Accuracy Dist: " + accuracyDist);
            if (accuracyDist > 0.00001 && accuracyDist < 580.1)
            {
                Debug.Log("Vi er inde i S if sætningens.");
                rating = ratingParent.transform.Find("S").gameObject;
            }
            if (accuracyDist > 580.1 && accuracyDist < 580.15)
            {
                rating = ratingParent.transform.Find("A").gameObject;
            }
            if (accuracyDist > 580.15 && accuracyDist < 580.17)
            {
                rating = ratingParent.transform.Find("B").gameObject;
            }
            if (accuracyDist > 580.17 && accuracyDist < 580.21)
            {
                rating = ratingParent.transform.Find("C").gameObject;
            }
            if (accuracyDist > 580.21 && accuracyDist < 580.24)
            {
                rating = ratingParent.transform.Find("D").gameObject;
            }
            if (accuracyDist > 580.24)
            {
                rating = ratingParent.transform.Find("F").gameObject;
            }
        }


        Debug.Log("Rating er valgt");

        rating.SetActive(true);

        Debug.Log("Rating er tændt");

        //Gør så vi kan kalde på ratingen udenfor funktionen for at slukke den i CutSceneInScene funktionen
        TheRating = rating;
        RatingManager.AddRating(rating);
    }

    //Opret en liste til at gemme ratings
    private static List<GameObject> ratingsList = new List<GameObject>();

    //Funktion til at tilføje en ratings til listen
    public static void AddRating(GameObject ratingObject)
    {
        ratingsList.Add(ratingObject);
    }

    //Funktion til at vise alle ratings
    public void DisplayRatings()
    {
        //Loop gennem alle ratings
        for (int i = 0; i < ratingsList.Count; i++)
        {
            //Beregn positionen af rating baseret på indeks
            Vector3 newPosition = textParent.position + Vector3.down * (vertikaleMellemrum * i);

            //Lav en kopi af den rating de fik (som vi gemte i listen)
            GameObject newRating = Instantiate(ratingsList[i], newPosition, Quaternion.identity, textParent);

            //Sæt rating objektets position
            newRating.transform.position = newPosition;

            //Tænd det nye rating objekt
            newRating.SetActive(true);
        }
    }
}
