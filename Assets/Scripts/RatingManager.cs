using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class RatingManager : MonoBehaviour
{
    public Transform textParent;
    public float vertikaleMellemrum = 50f;

    void Update()
    {

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

            //Lav en kope af den rating de fik (som vi gemte i listen)
            GameObject newRating = Instantiate(ratingsList[i], newPosition, Quaternion.identity, textParent);

            //Sæt rating objektets position
            newRating.transform.position = newPosition;

            //Tænd det nye rating objekt
            newRating.SetActive(true);
        }
    }
}
