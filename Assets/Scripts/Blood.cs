using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{

    public ParticleSystem waterdrops;
    public Material BloodPool;
    public float fadeDuration = 5f;
    private float initialSize;
    private float initialOpacity;
    float targetSize = 0.32f;
    float targetOpacity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        initialSize = 0f; // Set initial size value
        initialOpacity = 1f; // Set initial opacity value  
        BloodPool.SetFloat("_Size", initialSize);
        BloodPool.SetFloat("_Opacity", initialOpacity);
    }

    
    public IEnumerator Bleed()
    {
        float fadeSpeed = 1f / fadeDuration;

        // Fade indtil størrelsen når dens target value
        while (initialSize < targetSize)
        {
            //Beregn den nye størrelse baseret på den nuværende tid og fadespeed
            float newSize = Mathf.MoveTowards(initialSize, targetSize, fadeSpeed * Time.deltaTime);

            //Den får den nye størrelse
            BloodPool.SetFloat("_Size", newSize);

            //Wait for the next frame
            yield return null;

            //Opdatere the initial size value får næste iteration af while løkken
            initialSize = newSize;
        }

        //Sikrer at størrelsen ender på targetsize
        BloodPool.SetFloat("_Size", targetSize);
    }

    public IEnumerator FadeBlood()
    {
        // Calculate the rate at which opacity should change over time
        float fadeSpeed = 1f / fadeDuration;

        // Keep fading until the opacity reaches the target value
        while (initialOpacity > targetOpacity)
        {
            // Calculate the new opacity value based on the current time and fade speed
            float newOpacity = Mathf.MoveTowards(initialOpacity, targetOpacity, fadeSpeed * Time.deltaTime);

            // Set the new opacity value to the material
            BloodPool.SetFloat("_Opacity", newOpacity);
            
            // Wait for the next frame
            yield return null;

            // Update the initial opacity value for the next iteration
            initialOpacity = newOpacity;
        }

        // Ensure the opacity is set to the exact target value when the loop ends
        BloodPool.SetFloat("_Opacity", targetOpacity);

        Debug.Log("Blood has faded");
    }

    public void StartCoroutine()
    {
        StartCoroutine(Bleed());
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle collided with: " + other.name);
        if (other.CompareTag("Water"))
        {
            StartCoroutine(FadeBlood());
        }
        
    }
}
