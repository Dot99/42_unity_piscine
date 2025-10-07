using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenuButtons;
	public GameObject QuitConfirm;

	private bool isPaused = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPaused)
			{
				ResumeGame();
			}
			else
			{
				PauseGame();
			}
		}
	}

	public void ResumeGame()
	{
		pauseMenuButtons.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}

	public void PauseGame()
	{
		pauseMenuButtons.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}

	public void QuitGame()
	{
		QuitConfirm.SetActive(true);
		pauseMenuButtons.SetActive(false);
	}

	public void ConfirmQuit()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("Menu");
	}

	public void CancelQuit()
	{
		QuitConfirm.SetActive(false);
		pauseMenuButtons.SetActive(true);
	}
}