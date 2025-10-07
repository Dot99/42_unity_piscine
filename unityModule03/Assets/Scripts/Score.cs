using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI rankText;
	public GameObject continueButton;
	public GameObject replayButton;

	void Start()
	{
		scoreText.text = "Score: " + GameManager.Instance.score;
		rankText.text = "Rank: " + GameManager.Instance.GetRank();

		string previousScene = PlayerPrefs.GetString("LastScene", "map01");

		if (GameManager.Instance.gameOver)
		{
			continueButton.SetActive(false);
			replayButton.SetActive(true);
		}
		else
		{
			// Won → depends on which map
			if (previousScene == "map01")
			{
				continueButton.SetActive(true);
				replayButton.SetActive(false);
			}
			else if (previousScene == "map02")
			{
				continueButton.SetActive(false);
				replayButton.SetActive(false);
				SceneManager.LoadScene("WinScreen");
			}
		}
	}

	public void Replay()
	{
		SceneManager.LoadScene("map01");
	}

	public void NextLevel()
	{
		SceneManager.LoadScene("map02");
	}
}