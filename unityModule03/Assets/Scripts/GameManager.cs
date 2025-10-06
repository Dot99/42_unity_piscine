using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	public int baseHP = 5;
	public int energy = 100;

	public bool gameOver = false;

	public TextMeshProUGUI hpText;
	public TextMeshProUGUI energyText;

	void Awake()
	{
		Instance = this;
	}

	void Update()
	{
		if (hpText != null && energyText != null)
		{
			hpText.text = "Base HP: " + baseHP;
			energyText.text = "Energy: " + energy;
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

	public void HandleGameOver()
	{
		gameOver = true;
		Debug.Log("Game Over!");
		Spawner.Instance.StopSpawning();
		foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
		{
			Destroy(enemy.gameObject);
		}
	}
}
