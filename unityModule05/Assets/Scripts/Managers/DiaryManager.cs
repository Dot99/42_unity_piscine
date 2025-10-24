using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StageButton
{
	public int stageNumber;
	public Button button;
	public Image buttonImage;
	public Sprite unlockedSprite;
	public Sprite lockedSprite;
}

public class DiaryManager : MonoBehaviour
{
	[Header("UI References")]
	public TextMeshProUGUI totalLeavesText;
	public TextMeshProUGUI deathCountText;

	[Header("Stage Buttons")]
	public StageButton[] stageButtons;

	void Start()
	{
		// Load player progress
		int totalLeaves = PlayerPrefs.GetInt("TotalLeafPoints", 0);
		int deaths = PlayerPrefs.GetInt("DeathCount", 0);
		int unlockedStage = PlayerPrefs.GetInt("UnlockedStage", 1);

		// Update stats display
		totalLeavesText.text = $"Total Leaves: {totalLeaves}";
		deathCountText.text = $"Deaths: {deaths}";

		// Configure stage buttons
		foreach (StageButton sb in stageButtons)
		{
			bool isUnlocked = sb.stageNumber <= unlockedStage;

			// Enable/disable button interaction
			sb.button.interactable = isUnlocked;

			// Set the correct sprite
			if (sb.buttonImage != null)
				sb.buttonImage.sprite = isUnlocked ? sb.unlockedSprite : sb.lockedSprite;

			// Assign button listener
			sb.button.onClick.RemoveAllListeners(); // safety
			sb.button.onClick.AddListener(() => LoadStage(sb.stageNumber));
		}
	}

	void LoadStage(int stageNum)
	{
		GameManager.Instance.currentStage = stageNum;
		GameManager.Instance.isResuming = false;
		UIManager.Instance.UpdateHP(3);
		UIManager.Instance.UpdateScore(PlayerPrefs.GetInt("Score", 0));
		if (Application.CanStreamedLevelBeLoaded("Stage" + stageNum))
		{
			SceneManager.LoadScene("Stage" + stageNum);
		}
	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
