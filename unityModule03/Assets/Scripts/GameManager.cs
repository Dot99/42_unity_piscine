using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public WaveSpawner WaveSpawner;

	public int baseHP = 5;
	public int energy = 100;
	public int score = 0;

	public bool gameOver = false;

	public TextMeshProUGUI hpText;
	public TextMeshProUGUI energyText;

	void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		StartCoroutine(HandleWaves());
	}

	void Update()
	{
		if (hpText != null && energyText != null)
		{
			hpText.text = "Base HP: " + baseHP;
			energyText.text = "Energy: " + energy;
		}
	}

	private IEnumerator HandleWaves()
	{
		yield return StartCoroutine(WaveSpawner.StartWaves());
		while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
		{
			yield return null;
		}
		if (baseHP > 0)
		{
			gameOver = false;
			PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
			SceneManager.LoadScene("Score");
		}
		else
		{
			HandleGameOver();
		}
	}

	public bool SpendEnergy(int amount)
	{
		if (energy >= amount)
		{
			energy -= amount;
			return true;
		}
		return false;
	}

	public void GainEnergy(int amount)
	{
		energy += amount;
	}

	public void TakeDamage(int amount)
	{
		if (gameOver)
			return;
		baseHP -= amount;
		if (baseHP <= 0)
		{
			HandleGameOver();
		}
	}

	public void AddScore(int amount)
	{
		score += amount;
	}

	public string GetRank()
	{
		float hpRatio = (float)baseHP / 5f;
		float energyRatio = (float)energy / 100f;

		float total = (hpRatio + energyRatio) / 2f;
		if (total >= 0.95f && hpRatio == 1) return "S";
		if (total >= 0.9f) return "A";
		if (total >= 0.8f) return "B";
		if (total >= 0.7f) return "C";
		if (total >= 0.6f) return "D";
		return "F";
	}

	public void EndGame()
	{
		gameOver = false;
		PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
		SceneManager.LoadScene("Score");
	}

	public void WinGame()
	{
		gameOver = false;
		PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
		SceneManager.LoadScene("WinScreen");
	}

	public void HandleGameOver()
	{
		gameOver = true;
		Debug.Log("Game Over!");
		Spawner.Instance.StopSpawning();
		Time.timeScale = 1f;
		foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
		{
			Destroy(enemy.gameObject);
		}
		PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
		SceneManager.LoadScene("Score");
	}
}
