using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public GameObject turretPrefab;
	public int cost = 20;

	private GameObject previewTurret;

	private Image turretImage;

	void Start()
	{
		turretImage = GetComponent<Image>();
	}

	private void Update()
	{
		if (GameManager.Instance.energy < cost)
		{
			turretImage.color = new Color(1f, 0.5f, 0.5f, 0.7f);
		}
		else
		{
			turretImage.color = Color.white;
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (GameManager.Instance.energy < cost)
		{
			return;
		}
		previewTurret = Instantiate(turretPrefab, eventData.position, Quaternion.identity);
		previewTurret.GetComponent<SpriteRenderer>().color = Color.green;
		previewTurret.GetComponent<Collider2D>().enabled = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (previewTurret != null)
		{
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
			worldPos.z = 0;
			previewTurret.transform.position = worldPos;
		}
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		if (previewTurret == null) return;

		Collider2D previewCollider = previewTurret.GetComponent<Collider2D>();
		if (previewCollider != null)
			previewCollider.enabled = false;
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
		worldPos.z = 0;
		int buildSquareLayerMask = LayerMask.GetMask("BuildSquares");
		Collider2D hit = Physics2D.OverlapPoint(worldPos, buildSquareLayerMask);
		if (hit != null)
		{
			BuildSquare square = hit.GetComponent<BuildSquare>();
			if (square != null && !square.isOccupied)
			{
				bool spent = GameManager.Instance.SpendEnergy(cost);
				if (spent)
				{
					square.isOccupied = true;
					square.Highlight();
					Instantiate(turretPrefab, square.transform.position, Quaternion.identity);
				}
			}
		}
		previewTurret.GetComponent<Collider2D>().enabled = true;
		Destroy(previewTurret);
	}
}
