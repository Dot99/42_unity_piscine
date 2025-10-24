using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public int score = 0;
	public int hp = 3;
	public int currentStage = 1;
	public int unlockedStage = 1;
	public bool isResuming = false;
	public GameObject uiPrefab;

	private UIManager uiManager;


	void Awake()
	{
		// Singleton pattern
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject); // Keep across scenes
	}

	void Start()
	{
		if (UIManager.Instance == null && uiPrefab != null)
		{
			GameObject ui = Instantiate(uiPrefab);
			uiManager = ui.GetComponent<UIManager>();
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			RestartStage();
		}
	}

	public void AddScore(int amount)
	{
		score += amount;
		PlayerPrefs.SetInt("TotalLeafPoints", PlayerPrefs.GetInt("TotalLeafPoints", 0) + amount); // track lifetime points
		SaveProgress();
		if (UIManager.Instance != null)
			UIManager.Instance.UpdateScore(score);
	}

	public void ResetScore()
	{
		score = 0;
		if (UIManager.Instance != null)
			UIManager.Instance.UpdateScore(score);
	}

	public void LoadNextStage()
	{
		currentStage++;
		string nextScene = "Stage" + currentStage;

		if (Application.CanStreamedLevelBeLoaded(nextScene))
		{
			UnlockNextStage();
			UIManager.Instance.UpdateHP(3);
			SceneManager.LoadScene(nextScene);
		}
		else
		{
			Debug.Log("All stages completed!");
			SceneManager.LoadScene("MainMenu");
		}
	}

	public void SaveProgress()
	{
		PlayerController player = FindFirstObjectByType<PlayerController>();
		if (player != null)
		{
			Vector3 pos = player.transform.position;
			PlayerPrefs.SetFloat("PlayerPosX", pos.x);
			PlayerPrefs.SetFloat("PlayerPosY", pos.y);
			PlayerPrefs.SetFloat("PlayerPosZ", pos.z);
		}
		PlayerPrefs.SetInt("HP", hp);
		PlayerPrefs.SetInt("Score", score);
		PlayerPrefs.SetInt("UnlockedStage", unlockedStage);
		PlayerPrefs.SetInt("CurrentStage", currentStage);
		PlayerPrefs.Save();
	}

	public void ResumeProgress()
	{
		hp = PlayerPrefs.GetInt("HP", 3);
		UIManager.Instance.UpdateHP(hp);
		score = PlayerPrefs.GetInt("Score", 0);
		UIManager.Instance.UpdateScore(score);
		unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);
		currentStage = PlayerPrefs.GetInt("CurrentStage", 1);
	}
	public Vector3 GetSavedPlayerPosition()
	{
		if (PlayerPrefs.HasKey("PlayerPosX"))
		{
			float x = PlayerPrefs.GetFloat("PlayerPosX");
			float y = PlayerPrefs.GetFloat("PlayerPosY");
			float z = PlayerPrefs.GetFloat("PlayerPosZ");
			return new Vector3(x, y, z);
		}

		// Default: if no saved position, start at the StartPoint
		StartPoint start = FindFirstObjectByType<StartPoint>();
		return start != null ? start.transform.position : Vector3.zero;
	}
	public void UnlockNextStage()
	{
		if (currentStage >= unlockedStage)
		{
			unlockedStage = currentStage + 1;
			PlayerPrefs.SetInt("UnlockedStage", unlockedStage);
			PlayerPrefs.Save();
		}
	}

	public void RestartStage()
	{
		hp = 3;
		if (UIManager.Instance != null)
			UIManager.Instance.UpdateHP(3);
		Scene currentScene = SceneManager.GetActiveScene();
		if (currentScene.name == "MainMenu" || currentScene.name == "Diary")
			return;
		SceneManager.LoadScene(currentScene.name);
	}

}

