using UnityEngine;

public class ExitZone : MonoBehaviour
{
	public string exitId;
	public Transform requiredCharacter;
	public float alignmentThreshold = 0.5f;
	public bool isOccupied = false;

	private void Update()
	{
		if (requiredCharacter == null)
		{
			return;
		}
		Vector2 exitXZ = new Vector2(transform.position.x, transform.position.z);
		Vector2 charXZ = new Vector2(requiredCharacter.position.x, requiredCharacter.position.z);
		float distance = Vector2.Distance(exitXZ, charXZ);

		bool wasOccupied = isOccupied;
		isOccupied = distance < alignmentThreshold;

		if (isOccupied != wasOccupied)
		{
			GameManager.Instance.CheckForCompletion();
		}
	}
}
