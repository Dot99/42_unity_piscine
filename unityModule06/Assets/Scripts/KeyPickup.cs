using UnityEngine;

public class KeyPickup : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (GameManager.instance != null)
			{
				GameManager.instance.AddKey();
			}
			Destroy(gameObject);
		}
	}
}
