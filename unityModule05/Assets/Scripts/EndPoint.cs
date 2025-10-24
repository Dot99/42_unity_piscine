using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
	public TextMeshProUGUI messageText;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Scene currentScene = SceneManager.GetActiveScene();
			if (currentScene.name == "Stage1" && GameManager.Instance.score >= 25)
			{
				GameManager.Instance.LoadNextStage();
			}
			else if (currentScene.name == "Stage2" && GameManager.Instance.score >= 50)
			{
				GameManager.Instance.LoadNextStage();
			}
			else if (currentScene.name == "Stage3" && GameManager.Instance.score >= 75)
			{
				Debug.Log("Game Completed! Returning to Main Menu.");
				SceneManager.LoadScene("MainMenu");
			}
			else
			{
				if (messageText != null)
					messageText.text = "You need to collect more leaves to proceed!";
			}
		}
	}
}
