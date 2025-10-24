using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public void NewGame()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
		GameManager.Instance.hp = 3;
		GameManager.Instance.score = 0;
		GameManager.Instance.unlockedStage = 1;
		GameManager.Instance.currentStage = 1;
		GameManager.Instance.isResuming = false;
		GameManager.Instance.ResetScore();
		UIManager.Instance.UpdateHP(GameManager.Instance.hp);
		SceneManager.LoadScene("Stage1");
	}

	public void Resume()
	{
		GameManager.Instance.ResumeProgress();
		GameManager.Instance.isResuming = true;
		string stageName = "Stage" + GameManager.Instance.currentStage;
		SceneManager.LoadScene(stageName);
	}

	public void OpenDiary()
	{
		SceneManager.LoadScene("Diary");
	}
}
