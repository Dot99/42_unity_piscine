using UnityEngine;


public class Cactus : MonoBehaviour
{
	public GameObject jellyPrefab;
	public AudioClip shootSound;
	public Transform shootPoint;

	public float jellySpeed = 5f;

	private AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			Shoot();
		}
	}
	void Shoot()
	{
		audioSource.PlayOneShot(shootSound);
		GameObject jelly = Instantiate(jellyPrefab, shootPoint.position, Quaternion.identity);
		Vector3 position = jelly.transform.position;
		position.z = -1;
		jelly.transform.position = position;
		Rigidbody2D rb = jelly.GetComponent<Rigidbody2D>();
		rb.linearVelocity = Vector2.right * jellySpeed;
		Collider2D cactusCollider = GetComponent<Collider2D>();
		Collider2D jellyCollider = jelly.GetComponent<Collider2D>();
		if (cactusCollider != null && jellyCollider != null)
		{
			Physics2D.IgnoreCollision(cactusCollider, jellyCollider);
		}
	}
}
