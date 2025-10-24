using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[Header("HUD Elements")]
	public TextMeshProUGUI hpText;
	public TextMeshProUGUI scoreText;

	private Canvas canvas;

	private void Awake()
	{
		// Singleton pattern to persist this UI
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
		canvas = GetComponentInChildren<Canvas>(true);
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		// Hide the UI in MainMenu and Diary scenes
		if (scene.name == "MainMenu" || scene.name == "Diary")
		{
			if (canvas != null)
				canvas.enabled = false;
		}
		else
		{
			if (canvas != null)
				canvas.enabled = true;
		}
	}

	public void UpdateHP(int hp)
	{
		hpText.text = "HP: " + hp;
	}

	public void UpdateScore(int points)
	{
		scoreText.text = "Leaves: " + points;
	}

	public void ResetStageValues()
	{
		UpdateHP(3);
		UpdateScore(0);
	}
}
