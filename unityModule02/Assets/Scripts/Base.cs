using UnityEngine;

public class Base : MonoBehaviour
{
	public static Base Instance;
	public int health = 5;

	void Awake()
	{
		Instance = this;
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		Debug.Log("Base Health: " + health);
		if (health <= 0)
		{
			GameOver();
		}
	}

	void GameOver()
	{
		Debug.Log("Game Over!");
		Spawner.Instance.StopSpawning();
		foreach (var enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
		{
			Destroy(enemy.gameObject);
		}
	}
}