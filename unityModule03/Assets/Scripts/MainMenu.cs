using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene("map01");
	}

	public void QuitGame()
	{
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
