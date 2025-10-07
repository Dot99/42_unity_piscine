using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
	[System.Serializable]
	public class Wave
	{
		public GameObject enemyPrefab;
		public int enemiesPerWave = 10;
		public float spawnInterval = 1f;
	}

	public Wave[] waves;
	public Transform spawnPoint;
	public float timeBetweenWaves = 3f;

	private int currentWaveIndex = 0;

	public bool AllWavesFinished => currentWaveIndex >= waves.Length;

	public IEnumerator StartWaves()
	{
		for (currentWaveIndex = 0; currentWaveIndex < waves.Length; currentWaveIndex++)
		{
			Wave currentWave = waves[currentWaveIndex];

			yield return StartCoroutine(SpawnWave(currentWave));

			yield return new WaitForSeconds(timeBetweenWaves);
		}
	}

	private IEnumerator SpawnWave(Wave wave)
	{
		for (int i = 0; i < wave.enemiesPerWave; i++)
		{
			Instantiate(wave.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
			yield return new WaitForSeconds(wave.spawnInterval);
		}
	}
}
