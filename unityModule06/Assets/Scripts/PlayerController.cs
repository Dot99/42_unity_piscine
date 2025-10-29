using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 3f;
	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(h, 0f, v).normalized;

		if (movement.magnitude > 0)
		{
			animator.SetBool("isWalking", true);
			transform.Translate(movement * speed * Time.deltaTime, Space.World);
			transform.forward = movement;
		}
		else
		{
			animator.SetBool("isWalking", false);
		}
	}
}
