using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public Vector3 targetScale;  // Target scale to reach
    public float duration = 1.0f; // Time to reach the target scale

    private Vector3 initialScale; // Starting scale of the object
    private float timeElapsed = 0.0f; // Time counter
    public bool isScaling = false; // Scaling flag

    void Start()
    {
        initialScale = transform.localScale;  // Store the object's initial scale
    }

    void Update()
    {
        if (isScaling)
        {
            // Increment the time elapsed based on frame time
            timeElapsed += Time.deltaTime;

            // Calculate the interpolation factor (0 to 1)
            float t = timeElapsed / duration;

            // Lerp the scale from initial to target based on the factor
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            // If the scaling is done, stop updating
            if (t >= 1.0f)
            {
                isScaling = false;
            }
        }
    }

    public void StopScaling(bool goodKey)
    {
        if (goodKey)
        {
            float diff = transform.localScale.x - targetScale.x;
            //Debug.Log($"{transform.localScale} {targetScale}");

            if (diff <= 0.5)
            {
                Debug.Log("win" + diff);
            }
            else
            {
                Debug.Log("lose" + diff);
            }
        }
        else
        {
            Debug.Log("lose, wrong key");
        }

        isScaling = false;
    }

    // Call this method to start the scaling
    public void StartScaling(float givenDuration)
    {
        transform.localScale = initialScale;  // Reset the scale to initial
        initialScale = transform.localScale;  // Refresh initial scale if the scale was changed
        timeElapsed = 0.0f;                   // Reset the time
        duration = givenDuration;             // Set the duration
        isScaling = true;                     // Set the flag to start scaling
    }
}
