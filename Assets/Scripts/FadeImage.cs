using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
    public Image image; // Assign the Image component in the inspector
    public float fadeDuration = 1.0f; // Duration of the fade effect
    public bool fadeOutOnStart = true; // Option to fade out at the start

    public UnityEvent onFadeInComplete; // Event triggered after fade-in finishes

    private bool isFading = false;

    private void Start()
    {
        // Ensure the image starts fully opaque
        Color color = image.color;
        color.a = 1.0f;
        image.color = color;

        // Optionally start fading out
        if (fadeOutOnStart)
        {
            FadeOut();
        }
    }

    public void FadeIn()
    {
        if (!isFading)
            StartCoroutine(Fade(1.0f, true)); // Fade to fully opaque
    }

    public void FadeOut()
    {
        if (!isFading)
            StartCoroutine(Fade(0.0f, false)); // Fade to fully transparent
    }

    private IEnumerator Fade(float targetAlpha, bool invokeEvent)
    {
        isFading = true;
        float startAlpha = image.color.a; // Get the current alpha value
        float timeElapsed = 0.0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);

            // Set the new alpha value
            Color color = image.color;
            color.a = newAlpha;
            image.color = color;

            yield return null;
        }

        // Ensure the final alpha is set to the target value
        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;

        // Trigger the UnityEvent if fading in
        if (invokeEvent && targetAlpha == 1.0f)
        {
            onFadeInComplete?.Invoke();
        }

        isFading = false;
    }
}
