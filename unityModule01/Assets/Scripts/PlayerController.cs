using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private static PlayerInputActions inputActions;
	private static GameObject activePlayer;
	private Rigidbody rb;
	private Camera mainCamera;

	public float moveSpeed = 3f;
	public float jumpForce = 5f;
	public bool isGrounded = true;
	public string characterId;

	public static class CharacterRegistry
	{
		public static GameObject[] Characters;
	}

	void Awake()
	{
		if (inputActions == null)
		{
			inputActions = new PlayerInputActions();
			inputActions.Enable();
		}
	}
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		mainCamera = Camera.main;
		CharacterRegistry.Characters = new GameObject[]
		{
				GameObject.Find("Blue"),
				GameObject.Find("Red"),
				GameObject.Find("Yellow")
		};
	}

	void Update()
	{
		if (activePlayer == gameObject)
		{
			float moveInputX = inputActions.Player.Move.ReadValue<float>();
			if (activePlayer == gameObject)
			{
				Vector3 velocity = rb.linearVelocity;
				velocity.x = moveInputX * moveSpeed;
				rb.linearVelocity = velocity;
			}
			if (inputActions.Player.Jump.triggered && isGrounded)
			{
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				isGrounded = false;
			}
			if (mainCamera != null)
			{
				Vector3 camPos = mainCamera.transform.position;
				camPos.x = transform.position.x;
				camPos.y = transform.position.y;
				mainCamera.transform.position = camPos;
			}
		}
		if (inputActions.CharSelection.SwitchTo1.triggered)
			SetActiveCharacter(0);
		if (inputActions.CharSelection.SwitchTo2.triggered)
			SetActiveCharacter(1);
		if (inputActions.CharSelection.SwitchTo3.triggered)
			SetActiveCharacter(2);
		if (inputActions.Reset.Reset.triggered)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.contacts[0].normal.y > 0.5f)
			isGrounded = true;
	}

	static void SetActiveCharacter(int index)
	{
		if (index >= 0 && index < CharacterRegistry.Characters.Length)
			activePlayer = CharacterRegistry.Characters[index];
	}
}
