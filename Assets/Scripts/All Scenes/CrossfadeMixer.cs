using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CrossfadeMixer : MonoBehaviour
{

	public AudioMixer mixer;
	public bool fading;

	public void CrossfadeGroups(float duration)
	{
		if (!fading)
		{
			StartCoroutine(Crossfade(duration));
		}
	}

	IEnumerator Crossfade(float fadeTime)
	{
		fading = true;
		float currentTime = 0;

		while (currentTime <= fadeTime)
		{
			currentTime += Time.deltaTime;

			mixer.SetFloat("volPadAll", Mathf.Log10(Mathf.Lerp(1, 0.0001f, currentTime / fadeTime)) * 20);
			mixer.SetFloat("volPadLow", Mathf.Log10(Mathf.Lerp(0.0001f, 1, currentTime / fadeTime)) * 20);

			yield return null;
		}

		fading = false;

	}
}