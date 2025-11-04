using UnityEngine;

public class GargoyleAI : MonoBehaviour
{
	public float detectionRange = 6f;
	private Transform player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		Vector3 directionToPlayer = player.position - transform.position;
		float distance = directionToPlayer.magnitude;
		if (distance < detectionRange && Vector3.Dot(transform.forward, directionToPlayer.normalized) > 0.7f)
		{
			AlertAllGhosts();
		}
	}

	void AlertAllGhosts()
	{
		GhostAI[] ghosts = FindObjectsByType<GhostAI>(FindObjectsSortMode.None);
		foreach (GhostAI ghost in ghosts)
		{
			ghost.StartChasing(player);
		}
	}
}
