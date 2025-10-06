using UnityEngine;

public class WaypointManager : MonoBehaviour
{
	public static WaypointManager Instance;
	public Transform[] waypoints;

	private void Awake()
	{
		Instance = this;
	}
}
