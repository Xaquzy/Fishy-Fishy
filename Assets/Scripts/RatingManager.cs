using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class RatingManager : MonoBehaviour
{
    //Rating ting
    public GameObject ratingParent;
    public GameObject FinalRating;
    [HideInInspector] public GameObject TheRating; // Et GameObject der starter tomt men senere s�ttes den til at v�re ratingen. Derefter kan ratingen kaldes i udenfor RatingManager og slukkes i CutScene 
    public GameObject TomRatingPlaceholder;
    public Drawing Drawing;
    public DropObjZone DropObjZone;
    public Transform textParent;
    public float vertikaleMellemrum = 50f;
    public float horizontalMellemrum = 100f;
    private int TotalRatingScore = 0;
    private bool finalRatingSet = false;

    public AudioSource RatingAudioGood;
    public AudioSource RatingAudioBad;

    void Update()
    {

    }
    //Funktion til at bestemme rating
    public void Rating()
    {
        Debug.Log("Vi er inde i rating funktionen.");
        float accuracyDist = Drawing.GetAccuracyDist(); //F� adgang til accuracyDist
        ratingParent.SetActive(true);

        //Deaktiver alle rating beskeder
        for (int i = 0; i < ratingParent.transform.childCount; i++)
        {
            Transform t = ratingParent.transform.GetChild(i);
            t.gameObject.SetActive(false);
        }
        GameObject rating = null;

        //Ratings for drab/drop
        if (float.IsNaN(accuracyDist)) //Hvis accucacy ikke er et tal (NaN). Vi bestemte dette ved at debug.log for at finde ud af hvad vi skal hae i if-s�tningen ud fra accacydist. vi startede med if accuracyDist = null
        {
            if (DropObjZone.ZoneScore >= 5)
            {
                rating = ratingParent.transform.Find("F").gameObject;
                TotalRatingScore += 0;
            }
            if (DropObjZone.ZoneScore == 4)
            {
                rating = ratingParent.transform.Find("D").gameObject;
                TotalRatingScore += 4;
                
            }
            if (DropObjZone.ZoneScore == 3)
            {
                rating = ratingParent.transform.Find("C").gameObject;
                TotalRatingScore += 7;
            }
            if (DropObjZone.ZoneScore == 2)
            {
                rating = ratingParent.transform.Find("B").gameObject;
                TotalRatingScore += 10;
            }
            if (DropObjZone.ZoneScore == 1)
            {
                rating = ratingParent.transform.Find("A").gameObject;
                TotalRatingScore += 12;
            }
            if (DropObjZone.ZoneScore == 0)
            {
                rating = ratingParent.transform.Find("S").gameObject;
                TotalRatingScore += 13;
            }

            if (RatingAudioGood!=null)
            {
                if (DropObjZone.ZoneScore > 2)
                {
                    RatingAudioBad.Play();
                }
                else
                {
                    RatingAudioGood.Play();
                }
            }
           
        }

        //Ratings for tegne
        else
        {
            Debug.Log("Accuracy Dist: " + accuracyDist);
            if (accuracyDist > 0.00001 && accuracyDist <= 0.1)
            {
                rating = ratingParent.transform.Find("S").gameObject;
                TotalRatingScore += 13;
            }
            if (accuracyDist > 0.1 && accuracyDist <= 0.15)
            {
                rating = ratingParent.transform.Find("A").gameObject;
                TotalRatingScore += 12;
            }
            if (accuracyDist > 0.15 && accuracyDist <= 0.2)
            {
                rating = ratingParent.transform.Find("B").gameObject;
                TotalRatingScore += 10;
            }
            if (accuracyDist > 0.2 && accuracyDist <= 0.25)
            {
                rating = ratingParent.transform.Find("C").gameObject;
                TotalRatingScore += 7;
            }
            if (accuracyDist > 0.25 && accuracyDist <= 0.3)
            {
                rating = ratingParent.transform.Find("D").gameObject;
                TotalRatingScore += 4;
            }
            if (accuracyDist > 0.3)
            {
                rating = ratingParent.transform.Find("F").gameObject;
                TotalRatingScore += 0;
            }

            if (RatingAudioGood != null)
            {
                if (accuracyDist > 0.2)
                {
                    RatingAudioBad.Play();
                }
                else
                {
                    RatingAudioGood.Play();
                }
            }
        }

        if (rating == null)
        {
            rating = TomRatingPlaceholder;
        }


        Debug.Log("Rating er valgt");

        rating.SetActive(true);

        Debug.Log("Rating er t�ndt");

        //G�r s� vi kan kalde p� ratingen udenfor funktionen for at slukke den i CutSceneInScene funktionen
        TheRating = rating;
        RatingManager.AddRating(rating);
    }

    //Opret en liste til at gemme ratings
    private static List<GameObject> ratingsList = new List<GameObject>();

    //Funktion til at tilf�je en ratings til listen
    public static void AddRating(GameObject ratingObject)
    {
        ratingsList.Add(ratingObject);
    }

    //Funktion til at vise alle ratings
    public void DisplayRatings()
    {
    //    int kolonne = 0;
    //    int r�kke = 0;
    //    //Loop gennem alle ratings
    //    for (int i = 0; i < ratingsList.Count; i++)
    //    {
    //        //Beregn positionen af rating baseret p� indeks
    //        Vector3 newPosition = textParent.position + Vector3.down * (vertikaleMellemrum * r�kke) + Vector3.right * (horizontalMellemrum * kolonne);

    //        //Lav en kopi af den rating de fik (som vi gemte i listen)
    //        GameObject newRating = Instantiate(ratingsList[i], newPosition, Quaternion.identity, textParent);

    //        //S�t rating objektets position
    //        newRating.transform.position = newPosition;

    //        //T�nd det nye rating objekt
    //        newRating.SetActive(true);

    //        if ((i + 1) % 5 == 0) 
    //        {
    //            kolonne = kolonne + 1;
    //            r�kke = 0;
    //        }
    //   }

        GameObject finalRating = null;

        if (!finalRatingSet)
        {
            if (TotalRatingScore >= 108)
            {
                finalRating = FinalRating.transform.Find("Final S").gameObject;
            }

            if (TotalRatingScore < 108 && TotalRatingScore >= 92)
            {
                finalRating = FinalRating.transform.Find("Final A").gameObject;
            }

            if (TotalRatingScore < 92 && TotalRatingScore >= 66)
            {
                finalRating = FinalRating.transform.Find("Final B").gameObject;
            }

            if (TotalRatingScore < 66 && TotalRatingScore >= 39)
            {
                finalRating = FinalRating.transform.Find("Final C").gameObject;
            }

            if (TotalRatingScore < 39 && TotalRatingScore > 4)
            {
                finalRating = FinalRating.transform.Find("Final D").gameObject;
            }

            if (TotalRatingScore <= 4)
            {
                finalRating = FinalRating.transform.Find("Final F").gameObject;
            }
            finalRating.SetActive(true);

            finalRatingSet = true;
        }
    }
        
}
