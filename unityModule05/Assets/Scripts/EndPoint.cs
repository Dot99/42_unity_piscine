using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
	public TextMeshProUGUI messageText;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (GameManager.Instance.score >= 25)
			{
				GameManager.Instance.LoadNextStage();
			}
			else
			{
				if (messageText != null)
					messageText.text = "You need at least 25 points to advance!";
			}
		}
	}
}
