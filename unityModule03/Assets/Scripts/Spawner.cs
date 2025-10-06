using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public static Spawner Instance;
	public GameObject enemyPrefab;
	public float spawnDelay = 2f;
	private bool spawning = true;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		StartCoroutine(SpawnEnemies());
	}

	IEnumerator SpawnEnemies()
	{
		while (spawning)
		{
			Instantiate(enemyPrefab, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(spawnDelay);
		}
	}

	public void StopSpawning()
	{
		spawning = false;
	}
}