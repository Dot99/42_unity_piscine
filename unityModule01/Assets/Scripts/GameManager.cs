using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public ExitZone[] exits;
	public int nextSceneName;
	private bool stageCompleted = false;

	private void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Scene stage1 = SceneManager.GetSceneByName("Stage1");
		if (stage1.IsValid() && stage1.isLoaded)
		{
			SceneManager.SetActiveScene(stage1);
		}
	}

	public void LoadNextStage()
	{
		Debug.Log("Loading next stage...");
		SceneManager.LoadScene(nextSceneName);
	}

	public void CheckForCompletion()
	{
		if(stageCompleted) return;
		bool allAligned = true;

		foreach (var exit in exits)
		{
			if (!exit.isOccupied)
			{
				allAligned = false;
				return;
			}
		}

		if (allAligned)
		{
			stageCompleted = true;
			Debug.Log("Stage Complete!");
			Invoke(nameof(LoadNextStage), 1f);
		}
		else
		{
			Debug.Log("Good alignment!");
		}
	}
}
