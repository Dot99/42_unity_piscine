using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 2f;
	public float health;

	public int scoreValue = 10;
	private readonly float maxHealth = 3f;

	private Transform targetWaypoint;
	private int waypointIndex = 0;

	void Start()
	{
		health = maxHealth;
		targetWaypoint = WaypointManager.Instance.waypoints[waypointIndex];
	}

	void Update()
	{
		if (targetWaypoint == null) return;
		Vector3 dir = targetWaypoint.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.1f)
		{
			GetNextWaypoint();
		}
	}

	void GetNextWaypoint()
	{
		waypointIndex++;
		if (waypointIndex >= WaypointManager.Instance.waypoints.Length)
		{
			GameManager.Instance.TakeDamage(1);
			Destroy(gameObject);
			return;
		}
		targetWaypoint = WaypointManager.Instance.waypoints[waypointIndex];
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{
			GameManager.Instance.GainEnergy(10);
			GameManager.Instance.AddScore(scoreValue);
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Base"))
		{
			GameManager.Instance.TakeDamage(1);
			Destroy(gameObject);
		}
		else if (other.CompareTag("Limit"))
		{
			Destroy(gameObject);
		}
	}
}