using UnityEngine;
using System.Collections.Generic;

public class Turret : MonoBehaviour
{
	public float fireRate = 1f;
	public float baseDamage = 0.1f;

	public float range = 3f;
	public GameObject bulletPrefab;

	private float fireCooldown = 0f;
	private List<Enemy> enemiesInRange = new List<Enemy>();

	void Update()
	{
		fireCooldown -= Time.deltaTime;
		Enemy target = GetClosestEnemy();
		if (target != null && fireCooldown <= 0f)
		{
			Shoot(target);
			fireCooldown = 1f / fireRate;
		}
	}

	void Start()
	{
		CircleCollider2D rangeCollider = GetComponent<CircleCollider2D>();
		rangeCollider.radius = range;
	}

	Enemy GetClosestEnemy()
	{
		Enemy closestEnemy = null;
		float minDistance = float.MaxValue;
		enemiesInRange.RemoveAll(e => e == null);

		foreach (var enemy in enemiesInRange)
		{
			float dist = Vector2.Distance(transform.position, enemy.transform.position);
			if (dist < minDistance)
			{
				minDistance = dist;
				closestEnemy = enemy;
			}
		}
		return closestEnemy;
	}

	void Shoot(Enemy target)
	{
		Vector3 direction = (target.transform.position - transform.position).normalized;
		Vector3 spawnPos = transform.position + direction * 0.5f; // 0.5 units offset

		GameObject bulletGO = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
		Bullet bullet = bulletGO.GetComponent<Bullet>();
		bullet.damage = baseDamage;
		bullet.SetTarget(target.transform);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			enemiesInRange.Add(other.GetComponent<Enemy>());
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			enemiesInRange.Remove(other.GetComponent<Enemy>());
		}
	}
}
