using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
	public void GoToMenu()
	{
		GameManager.Instance.SaveProgress();
		SceneManager.LoadScene("MainMenu");
	}
}
