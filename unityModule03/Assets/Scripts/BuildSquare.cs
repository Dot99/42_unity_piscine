using UnityEngine;

public class BuildSquare : MonoBehaviour
{
	public bool isOccupied = false;
	private SpriteRenderer sr;
	private Color defaultColor;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		defaultColor = sr.color;
	}

	public bool CanPlaceTurret()
	{
		return !isOccupied;
	}

	public void Highlight()
	{
		sr.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0.0f);
	}

	public void ResetColor()
	{
		sr.color = defaultColor;
	}
}
