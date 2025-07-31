using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public ExitZone[] exits;

	private void Awake()
	{
		Instance = this;
	}

	public void CheckForCompletion()
	{
		bool allAligned = true;

		foreach (var exit in exits)
		{
			if (!exit.isOccupied)
			{
				allAligned = false;
			}
		}

		if (allAligned)
		{
			Debug.Log("Stage Complete!");
		}
		else
		{
			Debug.Log("Good alignment!");
		}
	}
}
