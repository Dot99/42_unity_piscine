using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	public Vector3 moveDirection = Vector3.right;
	public float moveDistance = 5f;
	public float moveSpeed = 2f;

	private Vector3 startPos;
	private Vector3 targetPos;
	private Vector3 lastPosition;
	private Vector3 currentVelocity;

	public Vector3 CurrentVelocity => currentVelocity;

	void Start()
	{
		startPos = transform.position;
		targetPos = startPos + moveDirection.normalized * moveDistance;
		lastPosition = transform.position;
	}

	void Update()
	{
		// Move platform
		float pingPong = Mathf.PingPong(Time.time * moveSpeed, 1f);
		transform.position = Vector3.Lerp(startPos, targetPos, pingPong);

		// Calculate velocity
		currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
		lastPosition = transform.position;
	}
}
