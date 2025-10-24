using UnityEngine;
using UnityEngine.SceneManagement;

public class LeafCollectible : MonoBehaviour
{
	public int points = 5;

	void Start()
	{
		Scene currentScene = SceneManager.GetActiveScene();
		string key = currentScene.name + "_LeafCollected_" + gameObject.name;
		if (PlayerPrefs.HasKey(key))
		{
			gameObject.SetActive(false);
		}
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Scene currentScene = SceneManager.GetActiveScene();
			string key = currentScene.name + "_LeafCollected_" + gameObject.name;
			if (!PlayerPrefs.HasKey(key))
			{
				PlayerPrefs.SetInt(key, 1);
				GameManager.Instance.AddScore(5);
				GameManager.Instance.SaveProgress();
			}
			gameObject.SetActive(false);
		}
	}
}
