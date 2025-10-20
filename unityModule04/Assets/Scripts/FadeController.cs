using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 0); // transparent at start
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0, 1));
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1, 0));
    }

    private IEnumerator Fade(float start, float end)
    {
        float elapsed = 0;
        Color c = fadeImage.color;
        while (elapsed < fadeDuration)
        {
            c.a = Mathf.Lerp(start, end, elapsed / fadeDuration);
            fadeImage.color = c;
            elapsed += Time.deltaTime;
            yield return null;
        }
        c.a = end;
        fadeImage.color = c;
    }
}
