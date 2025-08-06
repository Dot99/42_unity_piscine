using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerController : MonoBehaviour
{
	private static int activeCharacterIndex = -1;
	private static PlayerInputActions inputActions;
	private static GameObject activePlayer;
	private GameObject[] sceneCharacters;
	private Rigidbody rb;
	private Camera mainCamera;
	private MovingPlatform currentPlatform;
	private bool cameraBlocked = false;

	public float moveSpeed = 3f;
	public float jumpForce = 5f;
	public bool isGrounded = true;
	public string characterId;

	void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        activePlayer = null;
        activeCharacterIndex = -1;
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

		var scene = gameObject.scene;
		var rootObjects = scene.GetRootGameObjects();

		sceneCharacters = new GameObject[3];
		foreach (var obj in rootObjects)
		{
			if (obj.name == "Blue") sceneCharacters[0] = obj;
			if (obj.name == "Red") sceneCharacters[1] = obj;
			if (obj.name == "Yellow") sceneCharacters[2] = obj;
		}
		if (activeCharacterIndex >= 0 && activeCharacterIndex < sceneCharacters.Length)
		{
			activePlayer = sceneCharacters[activeCharacterIndex];
		}
	}

	void Update()
	{
		if (activePlayer == gameObject && gameObject.scene == SceneManager.GetActiveScene())
		{
			float moveInputX = inputActions.Player.Move.ReadValue<float>();
			Vector3 velocity = rb.linearVelocity;
			velocity.x = moveInputX * moveSpeed;

			// Apply platform velocity if on one
			if (isGrounded && currentPlatform != null)
			{
				Vector3 platformVelocity = currentPlatform.CurrentVelocity;
				platformVelocity.y = 0;  // Ignore vertical movement
				velocity += platformVelocity;
			}

			rb.linearVelocity = velocity;
			if (inputActions.Player.Jump.triggered && isGrounded)
			{
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				isGrounded = false;
				currentPlatform = null;
			}
			var cameras = gameObject.scene.GetRootGameObjects()
							.SelectMany(go => go.GetComponentsInChildren<Camera>(true))
							.Where(cam => cam.CompareTag("MainCamera") && cam.enabled)
							.ToList();
			Camera currentCamera = cameras.FirstOrDefault();
			if (currentCamera != null && !cameraBlocked)
			{
				Vector3 camPos = currentCamera.transform.position;
				camPos.x = transform.position.x;
				camPos.y = transform.position.y;
				currentCamera.transform.position = camPos;
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
		{
			isGrounded = true;

			string platformTag = collision.gameObject.tag;

			if (
				(platformTag == "RedPlatform" && characterId != "Red") ||
				(platformTag == "BluePlatform" && characterId != "Blue") ||
				(platformTag == "YellowPlatform" && characterId != "Yellow")
			)
			{
				Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
				return;
			}

			// Only store the platform if it's correct
			if (collision.gameObject.TryGetComponent(out MovingPlatform platform))
			{
				currentPlatform = platform;
			}
		}
	}
	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.TryGetComponent(out MovingPlatform platform))
		{
			if (platform == currentPlatform)
			{
				currentPlatform = null;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("CameraBlocker"))
		{
			cameraBlocked = true;
			Debug.Log("Game Over!");
		}
	}

	static void SetActiveCharacter(int index)
	{
		activeCharacterIndex = index;
		var scene = SceneManager.GetActiveScene();
		var rootObjects = scene.GetRootGameObjects();

		GameObject charactersParent = null;
		foreach (var obj in rootObjects)
		{
			if (obj.name == "Characters")
			{
				charactersParent = obj;
				break;
			}
		}
		if (charactersParent == null)
			return;
		string targetName = index switch
		{
			0 => "Blue",
			1 => "Red",
			2 => "Yellow",
			_ => null
		};
		if (string.IsNullOrEmpty(targetName))
		{
			Debug.LogError($"Invalid character index {index}!");
			return;
		}
		Transform targetTransform = charactersParent.transform.Find(targetName);
		if (targetTransform == null)
			return;
		activePlayer = targetTransform.gameObject;
	}


	void OnDestroy()
	{
		if (SceneManager.GetActiveScene().isLoaded)
		{
			activePlayer = null;
		}
	}
}
