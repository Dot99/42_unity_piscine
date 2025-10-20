using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float jumpForce = 10f;

	public float hp = 3f;
	public Transform groundCheck;
	public LayerMask groundLayer;
	public int maxHp = 3;
	public Transform respawnPoint;
	public AudioClip jumpSound;
	public AudioClip damageSound;
	public AudioClip defeatSound;
	public AudioClip respawnSound;

	private Rigidbody2D rb;
	private Animator animator;
	private AudioSource audioSource;


	private bool isGrounded;
	private float moveInput;
	private Vector3 originalScale;
	private int currentHp;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		originalScale = transform.localScale;
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		currentHp = maxHp;
	}

	void Update()
	{
		moveInput = Input.GetAxis("Horizontal");
		animator.SetBool("isWalking", moveInput != 0);
		animator.SetBool("Dead", hp <= 0);

		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
		if (Input.GetButtonDown("Jump") && isGrounded)
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
			animator.SetTrigger("Jump");
			audioSource.PlayOneShot(jumpSound);
		}

		if (moveInput != 0)
		{
			transform.localScale = new Vector3(Mathf.Sign(moveInput) * Mathf.Abs(originalScale.x),
											   originalScale.y,
											   originalScale.z);
		}
		if (hp <= 0)
		{
			moveInput = 0;
			rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
			animator.SetTrigger("Dead");
		}
	}

	void FixedUpdate()
	{
		rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
	}

	public void TakeDamage(float damage)
	{
		hp -= damage;
		audioSource.PlayOneShot(damageSound);
		if (hp <= 0)
		{
			hp = 0;
			Die();
		}
		else
		{
			animator.SetTrigger("TakeDamage");
		}
	}

	void Die()
	{
		audioSource.PlayOneShot(defeatSound);
		FindFirstObjectByType<FadeController>().FadeOut();
		Invoke("Respawn", 2f);
	}

	void Respawn()
	{
		audioSource.PlayOneShot(respawnSound);
		FindFirstObjectByType<FadeController>().FadeIn();
		hp = maxHp;
		animator.SetTrigger("Respawn");
		transform.position = respawnPoint.position;
	}
}
