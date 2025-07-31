using UnityEngine;

public class ExitZone : MonoBehaviour
{
	public string exitId;
	public Transform requiredCharacter; // Reference to the matching cube
	public float alignmentThreshold = 0.2f; // How close they must be to count
	public bool isOccupied = false;

	private void Update()
	{
		if (requiredCharacter == null) return;

		float distance = Vector3.Distance(transform.position, requiredCharacter.position);
		bool wasOccupied = isOccupied;
		isOccupied = distance < alignmentThreshold;

		if (isOccupied != wasOccupied)
		{
			GameManager.Instance.CheckForCompletion();
		}
	}
}
