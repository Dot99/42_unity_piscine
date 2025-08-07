using UnityEngine;

public class Bullet : MonoBehaviour
{
	public string BulletColor;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerController player = other.GetComponent<PlayerController>();
			if (player != null && player.characterId == BulletColor)
			{
				Debug.Log("Game Over!");
			}
			else
			{
				Destroy(gameObject);
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
