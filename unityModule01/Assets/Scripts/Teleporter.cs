using UnityEngine;

public class Teleporter : MonoBehaviour
{
	public Transform teleportTarget;
	private void OnTriggerEnter(Collider other)
	{
		// Ensure only the player gets teleported
		if (other.CompareTag("Player"))
		{
			CharacterController controller = other.GetComponent<CharacterController>();
			if (controller != null)
			{
				// If using CharacterController, disable it before changing position
				controller.enabled = false;
				other.transform.position = teleportTarget.position;
				controller.enabled = true;
			}
			else
			{
				// For Rigidbody-based movement
				other.transform.position = teleportTarget.position;

				// Optional: reset velocity to avoid carrying unwanted speed
				Rigidbody rb = other.GetComponent<Rigidbody>();
				if (rb != null)
					rb.linearVelocity = Vector3.zero;
			}
		}
	}
}
