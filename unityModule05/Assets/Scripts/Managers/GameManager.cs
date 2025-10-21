using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public int score = 0;
	public int currentStage = 1;

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

	public void AddScore(int amount)
	{
		score += amount;
		Debug.Log("Score: " + score);
	}

	public void ResetScore()
	{
		score = 0;
	}

	public void LoadNextStage()
	{
		currentStage++;
		string nextScene = "Stage" + currentStage;

		if (Application.CanStreamedLevelBeLoaded(nextScene))
		{
			SceneManager.LoadScene(nextScene);
		}
		else
		{
			Debug.Log("All stages completed!");
			// You could load a win scene or restart
		}
	}
}

