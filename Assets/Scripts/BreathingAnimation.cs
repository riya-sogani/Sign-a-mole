using UnityEngine;

public class BreathingAnimation : MonoBehaviour
{
    public float scaleMultiplier = 0.1f;  // How much to scale up/down
    public float speed = 1f;             // Speed of the breathing cycle

    private Vector3 originalScale;

    void Start()
    {
        // Record the original scale of the object
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Create a breathing effect using a sine wave
        float scaleFactor = Mathf.Sin(Time.time * speed) * scaleMultiplier;
        transform.localScale = originalScale + new Vector3(scaleFactor, scaleFactor, 0);
    }
}