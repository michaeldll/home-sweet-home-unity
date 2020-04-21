using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CrossfadeMixer : MonoBehaviour
{

	public AudioMixer mixer;
	public bool fading;

	public void CrossfadeGroups(string vol1, string vol2, float duration)
	{
		if (!fading)
		{
			StartCoroutine(Crossfade(vol1, vol2, duration));
		}
	}

	IEnumerator Crossfade(string vol1, string vol2, float fadeTime)
	{
		fading = true;
		float currentTime = 0;

		while (currentTime <= fadeTime)
		{
			currentTime += Time.deltaTime;

			mixer.SetFloat(vol1, Mathf.Log10(Mathf.Lerp(1, 0.0001f, currentTime / fadeTime)) * 20);
			if (vol2 == "volPadHigh")
			{
				mixer.SetFloat(vol2, Mathf.Log10(Mathf.Lerp(0.0001f, 0.5f, currentTime / fadeTime)) * 20);
			}
			else
			{
				mixer.SetFloat(vol2, Mathf.Log10(Mathf.Lerp(0.0001f, 1, currentTime / fadeTime)) * 20);
			}

			yield return null;
		}

		fading = false;

	}
}