using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 0.5f;

	private PlayerInputActions inputActions;
	private Vector2 moveInput;
	private Rigidbody rb;

	void Awake()
	{
		inputActions = new PlayerInputActions();
	}

	void OnEnable()
	{
		inputActions.Player.Enable();
		inputActions.Player.Move.performed += OnMove;
		inputActions.Player.Move.canceled += OnMove;
	}

	void OnDisable()
	{
		inputActions.Player.Move.performed -= OnMove;
		inputActions.Player.Move.canceled -= OnMove;
		inputActions.Player.Disable();
	}

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	void Update()
	{
    	moveInput = inputActions.Player.Move.ReadValue<Vector2>();
	}

    void FixedUpdate()
    {
		if (moveInput == Vector2.zero)
    	{
        	rb.linearVelocity = Vector3.zero;
        	return;
    	}
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
		rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }

	private void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
	}
}
