using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
	public CinemachineCamera tpsCamera;
	public CinemachineCamera fpsCamera;
	public GameObject playerModel;
	public PlayerMovement playerMovement;

	private bool isFPS = false;

	void Start()
	{
		SetCameraMode(false); // Start in TPS
	}

	void Update()
	{
		var keyboard = Keyboard.current;
		if (keyboard.cKey.wasPressedThisFrame)
		{
			isFPS = !isFPS;
			SetCameraMode(isFPS);
		}
	}

	void SetCameraMode(bool fps)
	{
		if (fps)
		{
			fpsCamera.transform.rotation = tpsCamera.transform.rotation;
			tpsCamera.gameObject.SetActive(false);
			fpsCamera.gameObject.SetActive(true);
			playerModel.SetActive(false);

			playerMovement.cameraTransform = fpsCamera.transform;
			playerMovement.isFPS = true;
		}
		else
		{
			tpsCamera.transform.rotation = fpsCamera.transform.rotation;
			fpsCamera.gameObject.SetActive(false);
			tpsCamera.gameObject.SetActive(true);
			playerModel.SetActive(true);

			playerMovement.cameraTransform = tpsCamera.transform;
			playerMovement.isFPS = false;
		}

		tpsCamera.Priority = fps ? 0 : 10;
		fpsCamera.Priority = fps ? 10 : 0;
	}
}
