using UnityEngine;

[RequireComponent(typeof(Animator))]
public class VineAttack : MonoBehaviour
{
	public AudioClip attackSound;

	private Animator animator;
	private AudioSource audioSource;

	void Start()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			animator.SetBool("isAttacking", true);
			audioSource.PlayOneShot(attackSound);
			PlayerController player = other.GetComponent<PlayerController>();
			if (player != null)
			{
				player.TakeDamage(1);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			animator.SetBool("isAttacking", false);
		}
	}
}