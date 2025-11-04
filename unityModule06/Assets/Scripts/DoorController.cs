using UnityEngine;

public class DoorController : MonoBehaviour
{
	[Header("Door Settings")]
	public Transform pivot;
	public float openAngle = 90f;
	public float openSpeed = 2f;
	public bool openTowardsInside = true;

	[Header("Key Requirement")]
	public bool requiresKeys = false;
	public int keysNeeded = 3;

	private Quaternion closedRotation;
	private Quaternion openRotation;
	private bool isPlayerNear = false;
	private bool isOpen = false;
	private bool isUnlocked = false;

	void Start()
	{
		if (pivot == null)
			Debug.LogError("DoorController: Pivot not assigned!", this);

		closedRotation = pivot.localRotation;

		float angle = openTowardsInside ? openAngle : -openAngle;
		openRotation = closedRotation * Quaternion.Euler(0, angle, 0);
	}

	void Update()
	{
		if (isPlayerNear)
		{
			if (requiresKeys && !isUnlocked)
			{
				if (GameManager.keyCount >= keysNeeded)
					isUnlocked = true;
				else
					return;
			}
			// Open the door
			pivot.localRotation = Quaternion.Slerp(pivot.localRotation, openRotation, Time.deltaTime * openSpeed);
			if (Quaternion.Angle(pivot.localRotation, openRotation) < 1f)
				isOpen = true;
		}
		else if (isOpen)
		{
			pivot.localRotation = Quaternion.Slerp(pivot.localRotation, closedRotation, Time.deltaTime * openSpeed);
			if (Quaternion.Angle(pivot.localRotation, closedRotation) < 1f)
				isOpen = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			isPlayerNear = true;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
			isPlayerNear = false;
	}
}
