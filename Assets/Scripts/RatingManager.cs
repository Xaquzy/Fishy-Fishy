using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingManager : MonoBehaviour
{

    void Update()
    {
        //Debug.Log(ratings);
    }


    // Opret en liste til at gemme ratings
    private static List<string> ratings = new List<string>();

    // Funktion til at tilføje en ratings til listen
    public static void AddRating(string rating)
    {
        ratings.Add(rating);      
    }

    // Funktion til at vise alle ratings i slutningen af spillet (nu er det debug men det skal gøres med UI)
    public static void DisplayRatings()
    {
        foreach (string rating in ratings)
        {
            Debug.Log("Rating: " + rating);
        }
    }
}
