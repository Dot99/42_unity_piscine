using UnityEngine;

public class ColorButton : MonoBehaviour
{
	public string requiredCharacterId;
	public GameObject targetObject;

	private void OnTriggerEnter(Collider other)
	{
		PlayerController player = other.GetComponent<PlayerController>();
		if(player != null)
		{
			if(player.characterId == requiredCharacterId)
			{
				if(targetObject != null)
				{
					targetObject.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
