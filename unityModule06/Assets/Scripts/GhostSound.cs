using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GhostSound : MonoBehaviour
{
	public AudioClip ghostClip;
	public Transform player;
	public float maxDistance = 15f;
	[Range(0f, 1f)] public float maxVolume = 0.1f;

	private AudioSource ghostSource;

	void Start()
	{
		ghostSource = GetComponent<AudioSource>();
		ghostSource.clip = ghostClip;
		ghostSource.loop = true;
		ghostSource.spatialBlend = 1f;
		ghostSource.playOnAwake = false;
		ghostSource.dopplerLevel = 0f;
	}

	void Update()
	{
		if (!player) return;

		float dist = Vector3.Distance(transform.position, player.position);
		float volumeFactor = Mathf.Clamp01(1 - dist / maxDistance);
		ghostSource.volume = Mathf.Min(volumeFactor, maxVolume);
		if (volumeFactor > 0 && !ghostSource.isPlaying)
			ghostSource.Play();
		else if (volumeFactor <= 0 && ghostSource.isPlaying)
			ghostSource.Stop();
	}
}
