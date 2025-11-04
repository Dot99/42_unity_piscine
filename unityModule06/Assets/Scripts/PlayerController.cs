using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	public float speed = 3f;
	public Transform cameraTransform;
	public bool isFPS = false;

	private Animator animator;
	private Rigidbody rb;
	private Vector3 inputDir = Vector3.zero;
	private Vector3 cachedCamForward;
	private Vector3 cachedCamRight;


	void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		animator.applyRootMotion = false;
	}

	void Update()
	{
		var keyboard = Keyboard.current;
		float h = 0f;
		float v = 0f;

		if (keyboard.wKey.isPressed) v += 1;
		if (keyboard.sKey.isPressed) v -= 1;
		if (keyboard.aKey.isPressed) h -= 1;
		if (keyboard.dKey.isPressed) h += 1;

		bool isMoving = Mathf.Abs(h) > 0.01f || Mathf.Abs(v) > 0.01f;
		animator.SetBool("isWalking", isMoving);

		if (isMoving)
		{
			// Only capture camera yaw when movement begins
			if (cachedCamForward == Vector3.zero)
			{
				cachedCamForward = cameraTransform.forward;
				cachedCamRight = cameraTransform.right;
				cachedCamForward.y = 0;
				cachedCamRight.y = 0;
				cachedCamForward.Normalize();
				cachedCamRight.Normalize();
			}

			if (!isFPS) //TPS
			{
				Vector3 moveDir = (cachedCamForward * v + cachedCamRight * h).normalized;
				transform.position += moveDir * speed * Time.deltaTime;
				Quaternion targetRot = Quaternion.LookRotation(moveDir);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 180f * Time.deltaTime);
			}
			else //FPS
			{
				Vector3 moveDir = (cameraTransform.forward * v + cameraTransform.right * h).normalized;
				Vector3 newPos = rb.position + moveDir * speed * Time.deltaTime;
				rb.MovePosition(newPos);
				Vector3 lookDir = cameraTransform.forward;
				lookDir.y = 0;
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * 6f);
			}
		}
		else
		{
			animator.SetBool("isWalking", false);
			cachedCamForward = Vector3.zero;
			cachedCamRight = Vector3.zero;
			return;
		}
	}
}
