using UnityEngine;

public class Projectile : MonoBehaviour
{
	public GameObject projectile;
	public Transform firePoint;
	public float fireInterval = 2f;
	public float projectileSpeed = 10f;
	public string turretColor;

	private float lastFireTime = 0f;

	void Update()
	{
		if(Time.time - lastFireTime >= fireInterval)
		{
			Fire();
			lastFireTime = Time.time;
		}
	}

	void Fire()
	{
		GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);
		Rigidbody rb = proj.GetComponent<Rigidbody>();
		if(rb != null)
		{
			rb.linearVelocity = firePoint.forward * projectileSpeed;
		}

		Bullet bullet = proj.GetComponent<Bullet>();
		if(bullet != null)
		{
			bullet.BulletColor = turretColor;
		}
	}
}
