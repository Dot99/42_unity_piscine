using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlatform : MonoBehaviour
{
	public Vector3 moveDirection = Vector3.right;
	public float moveDistance = 5f;
	public float moveSpeed = 2f;

	private Vector3 startPos;
	private Vector3 targetPos;
	private Vector3 lastPosition;
	private Vector3 currentVelocity;
	private bool isActive = false;

	public Vector3 CurrentVelocity => currentVelocity;

	void Start()
	{
		startPos = transform.position;
		targetPos = startPos + moveDirection.normalized * moveDistance;
		lastPosition = transform.position;
		if (SceneManager.GetActiveScene().name != "Stage4" && SceneManager.GetActiveScene().name != "Stage5")
        {
            isActive = true;
        }
	}

	void Update()
	{
		if(!isActive) return;
		// Move platform
		float pingPong = Mathf.PingPong(Time.time * moveSpeed, 1f);
		transform.position = Vector3.Lerp(startPos, targetPos, pingPong);

		// Calculate velocity
		currentVelocity = (transform.position - lastPosition) / Time.deltaTime;
		lastPosition = transform.position;
	}

	public void Activate()
	{
		isActive = true;
	}
}
