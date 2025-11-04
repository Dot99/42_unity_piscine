using UnityEngine;

public class FootstepSound : MonoBehaviour
{
	public AudioSource footstepSource;
	public AudioClip footstepClips;
	public float stepInterval = 0.5f;

	private float stepTimer;
	private PlayerMovement player;

	void Start()
	{
		player = GetComponent<PlayerMovement>();
	}

	void Update()
	{
		if (player.GetComponent<Animator>().GetBool("isWalking"))
		{
			stepTimer += Time.deltaTime;
			if (stepTimer >= stepInterval)
			{
				footstepSource.PlayOneShot(footstepClips);
				stepTimer = 0f;
			}
		}
	}
}
