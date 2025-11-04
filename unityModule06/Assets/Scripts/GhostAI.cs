using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GhostAI : MonoBehaviour
{
	[Header("Patrol")]
	public Transform pointA;
	public Transform pointB;
	public float waitTime = 2f;
	private Transform currentTarget;
	private bool isWaiting = false;

	[Header("Chase")]
	public float chaseDuration = 3f;
	private bool isChasing = false;
	private bool returning = false;
	private float chaseTimer = 0f;

	[Header("References")]
	public Animator animator;

	[Header("Audio")]
	public AudioClip faintClip;
	public AudioSource sfxSource;

	private FadeUI fadeUI;
	private NavMeshAgent agent;
	private Transform player;
	private Vector3 startPos;

	void Start()
	{
		fadeUI = FindFirstObjectByType<FadeUI>();
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		startPos = transform.position;
		currentTarget = pointA;
	}

	void Update()
	{
		// Patrol behavior
		if (!isChasing && !returning)
		{
			if (!isWaiting)
			{
				agent.SetDestination(currentTarget.position);
				animator.SetBool("isWalking", true);

				if (Vector3.Distance(transform.position, currentTarget.position) < 0.5f)
				{
					StartCoroutine(WaitAtPoint());
					currentTarget = (currentTarget == pointA) ? pointB : pointA;
				}
			}
		}

		// Chase behavior
		if (isChasing)
		{
			chaseTimer += Time.deltaTime;
			agent.SetDestination(player.position);

			if (Vector3.Distance(transform.position, player.position) < 1.2f)
			{
				StartCoroutine(HandleCaughtPlayer());
			}

			if (chaseTimer >= chaseDuration)
			{
				isChasing = false;
				returning = true;
				agent.SetDestination(startPos);
				chaseTimer = 0f;
			}
		}

		// === Return to patrol ===
		if (returning)
		{
			agent.SetDestination(startPos);
			animator.SetBool("isWalking", true);
			if (Vector3.Distance(transform.position, startPos) < 0.5f)
				returning = false;
		}
	}

	IEnumerator WaitAtPoint()
	{
		isWaiting = true;
		animator.SetBool("isWalking", false);
		yield return new WaitForSeconds(waitTime);
		isWaiting = false;
	}

	private IEnumerator HandleCaughtPlayer()
	{
		sfxSource.PlayOneShot(faintClip);
		fadeUI.PlayCaughtFade();
		animator.SetBool("isWalking", false);
		agent.isStopped = true;
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}



	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isChasing = true;
			returning = false;
			chaseTimer = 0f;
		}
	}

	public void StartChasing(Transform playerTarget)
	{
		isChasing = true;
		returning = false;
		chaseTimer = 0f;
		player = playerTarget;
	}
}
