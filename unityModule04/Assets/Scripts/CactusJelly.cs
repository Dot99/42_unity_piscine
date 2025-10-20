using UnityEngine;

public class CactusJelly : MonoBehaviour
{
	public float damage = 1f;
	public float lifetime = 5f;

	void Start()
	{
		Destroy(gameObject, lifetime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerController player = other.GetComponent<PlayerController>();
			if (player != null)
			{
				player.TakeDamage(damage);
			}
		}
		if(other.CompareTag("Floor") || other.CompareTag("Player"))
		{
			Destroy(gameObject);
		}
	}
}
