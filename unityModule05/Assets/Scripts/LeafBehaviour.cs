using UnityEngine;

public class LeafCollectible : MonoBehaviour
{
	public int points = 5;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			GameManager.Instance.AddScore(points);
			Destroy(gameObject);
		}
	}
}
