using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CrossfadeMixer : MonoBehaviour
{

	[SerializeField] private AudioMixer mixer;
	[SerializeField] private bool fading;

	public void Crossfade(string vol1, float duration, string vol2 = null, float startVolume = 1f, float endVolume = 0.0001f)
	{
		if (!fading)
		{
			StartCoroutine(CrossfadeCoroutine(vol1, duration, vol2, startVolume, endVolume));
		}
	}

	IEnumerator CrossfadeCoroutine(string vol1, float duration, string vol2, float startVolume, float endVolume)
	{
		fading = true;
		float currentTime = 0;

		while (currentTime <= duration)
		{
			currentTime += Time.deltaTime;
			//Debug.Log(vol2);
			//Debug.Log(endVolume);
			//Debug.Log(startVolume);
			mixer.SetFloat(vol1, Mathf.Log10(Mathf.Lerp(startVolume, endVolume, currentTime / duration)) * 20);
			if (vol2 != null) mixer.SetFloat(vol2, Mathf.Log10(Mathf.Lerp(endVolume, startVolume, currentTime / duration)) * 20);

			yield return null;
		}

		fading = false;
	}
}