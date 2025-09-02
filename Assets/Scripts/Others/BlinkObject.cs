using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkObject : MonoBehaviour
{
    public float interval = 2f;       // how often to toggle
    public float startDelay = 0f;     // wait before toggling starts
    public float fadeDuration = 0.5f; // smooth fade speed

    private Renderer rend;
    private Collider2D col;
    private Color baseColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider2D>();

        if (rend != null)
            baseColor = rend.material.color;

        StartCoroutine(ToggleLoop());
    }

    IEnumerator ToggleLoop()
    {
        // wait before first toggle
        if (startDelay > 0f)
            yield return new WaitForSeconds(startDelay);

        while (true)
        {
            // Fade out
            yield return StartCoroutine(Fade(1f, 0f));
            if (col != null) col.enabled = false;
            yield return new WaitForSeconds(interval);

            // Fade in
            if (col != null) col.enabled = true;
            yield return StartCoroutine(Fade(0f, 1f));
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float alpha = Mathf.Lerp(from, to, t);

            if (rend != null)
            {
                Color c = baseColor;
                c.a = alpha;
                rend.material.color = c;
            }

            yield return null;
        }
    }
}
