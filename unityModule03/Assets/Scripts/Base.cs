using UnityEngine;

public class Base : MonoBehaviour
{
	public void TakeDamage(int damage)
	{
		GameManager.Instance.TakeDamage(-damage);
	}
}