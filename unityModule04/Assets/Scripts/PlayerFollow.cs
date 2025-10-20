using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
	public Transform player;
	public float followSpeed = 2f;

	private float fixedY;
	private float fixedZ;


	void Start()
	{
		fixedY = transform.position.y;
		fixedZ = transform.position.z;
	}
	void Update()
	{
		if (player != null)
		{
			Vector3 targetPosition = new Vector3(player.position.x, fixedY, fixedZ);
			transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
		}
	}
}
