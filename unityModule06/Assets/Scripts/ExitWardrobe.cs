using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitWardrobe : MonoBehaviour
{
	[Header("Audio")]
	public AudioClip winClip;
	public AudioSource sfxSource;

	private FadeUI fadeUI;

	public void Start()
	{
		fadeUI = FindFirstObjectByType<FadeUI>();
	}
	void OnTriggerEnter(Collider other)
	{
		sfxSource.PlayOneShot(winClip);
		fadeUI.PlayWinFade();
		if (other.CompareTag("Player"))
			SceneManager.LoadScene("Scene1"); //TODO: CHANGE THIS TO THE ACTUAL NEXT SCENE NAME
	}
}
