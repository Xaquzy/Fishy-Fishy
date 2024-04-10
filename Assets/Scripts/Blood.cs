using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{

    public ParticleSystem bloodParticles;
    public Material BloodPool;
    public float fadeDuration = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Bleed()
    {
        // Get the initial opacity value
        float initialSize = BloodPool.GetFloat("_Size");

        // Define the target opacity (0 in this case)
        float targetSize = 0.32f;

        // Calculate the rate at which opacity should change over time
        float fadeSpeed = 1f / fadeDuration;

        // Keep fading until the opacity reaches the target value
        while (initialSize < targetSize)
        {
            // Calculate the new opacity value based on the current time and fade speed
            float newSize = Mathf.MoveTowards(initialSize, targetSize, fadeSpeed * Time.deltaTime);

            // Set the new opacity value to the material
            BloodPool.SetFloat("_Size", newSize);

            // Wait for the next frame
            yield return null;

            // Update the initial opacity value for the next iteration
            initialSize = newSize;
        }

        // Ensure the opacity is set to the exact target value when the loop ends
        BloodPool.SetFloat("_Size", targetSize);
    }

    public IEnumerator FadeBlood()
    {
        // Get the initial opacity value
        float initialOpacity = BloodPool.GetFloat("_Opacity");

        // Define the target opacity (0 in this case)
        float targetOpacity = 0f;

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
    }

    public void StartCoroutine()
    {
        StartCoroutine(Bleed());
        //StartCoroutine(FadeBlood());
    }
}