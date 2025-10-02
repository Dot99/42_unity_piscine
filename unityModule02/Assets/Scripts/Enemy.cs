using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 2f;
	public float health;
	private float maxHealth = 3f;

	void Awake()
	{
		health = maxHealth;
	}

	void Update()
	{
		transform.Translate(Vector2.down * speed * Time.deltaTime);
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Base"))
		{
			Base.Instance.TakeDamage(1);
			Destroy(gameObject);
		}
		else if (other.CompareTag("Limit"))
		{
			Destroy(gameObject);
		}
	}
}