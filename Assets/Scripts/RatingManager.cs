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

    // Opret en liste til at gemme ratings
    private static List<GameObject> ratingsList = new List<GameObject>();

    // Funktion til at tilføje en ratings til listen
    public static void AddRating(GameObject ratingObject)
    {
        ratingsList.Add(ratingObject);
    }


    public void DisplayRatings()
    {
        // Loop through all ratings
        for (int i = 0; i < ratingsList.Count; i++)
        {
            // Set the position of the text based on index
            Vector3 newPosition = textParent.position + Vector3.down * (vertikaleMellemrum * i);

            // Instantiate a new rating object
            GameObject newRating = Instantiate(ratingsList[i], newPosition, Quaternion.identity, textParent);

            newRating.transform.position = newPosition;


            // Ensure the new rating object is active
            newRating.SetActive(true);
        }
    }
}
