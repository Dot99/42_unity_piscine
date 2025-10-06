using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 10f;
	public float damage = 0.1f;

	private Transform target;

	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

	void Update()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector2 direction = (target.position - transform.position).normalized;
		transform.Translate(direction * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			Enemy enemy = other.GetComponent<Enemy>();
			enemy?.TakeDamage(damage);
			Destroy(gameObject);
		}
	}
}