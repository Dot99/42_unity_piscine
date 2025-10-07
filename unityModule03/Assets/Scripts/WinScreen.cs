using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
	public void BackToMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}