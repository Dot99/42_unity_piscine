using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeUI : MonoBehaviour
{
	[Header("UI References")]
	public Image blackOverlay;
	public Image messageImage;

	[Header("Sprites")]
	public Sprite caughtSprite;
	public Sprite winSprite;

	[Header("Fade Settings")]
	public float fadeDuration = 1.5f;
	public float displayDuration = 2f;

	private Coroutine currentRoutine;

	public void PlayCaughtFade()
	{
		if (currentRoutine != null) StopCoroutine(currentRoutine);
		currentRoutine = StartCoroutine(FadeSequence(caughtSprite));
	}

	public void PlayWinFade()
	{
		if (currentRoutine != null) StopCoroutine(currentRoutine);
		currentRoutine = StartCoroutine(FadeSequence(winSprite));
	}

	private IEnumerator FadeSequence(Sprite message)
	{
		// Fade in black
		StartCoroutine(FadeImage(blackOverlay, 0f, 1f));

		// Show message
		messageImage.sprite = message;
		yield return StartCoroutine(FadeImage(messageImage, 0f, 1f));

		// Wait
		yield return new WaitForSeconds(displayDuration);

		// 4Fade out message and black
		StartCoroutine(FadeImage(messageImage, 1f, 0f));
		StartCoroutine(FadeImage(blackOverlay, 1f, 0f));
		yield return null;
	}

	private IEnumerator FadeImage(Image img, float from, float to)
	{
		float t = 0;
		Color c = img.color;
		while (t < fadeDuration)
		{
			t += Time.deltaTime;
			c.a = Mathf.Lerp(from, to, t / fadeDuration);
			img.color = c;
			yield return null;
		}
		c.a = to;
		img.color = c;
	}
}
