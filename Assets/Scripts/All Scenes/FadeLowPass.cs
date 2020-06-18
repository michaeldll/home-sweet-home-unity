using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FadeLowPass : MonoBehaviour
{
	[SerializeField] private AudioMixer mixer;
	private bool _fading = false;
	private float _smoothTime = 0.3f;
	private float _yVelocity = 0.0f;
	private float _frequencyLerped = 2800;

	public void Fade(string lowPassName, float duration, float toFrequency = 450, float fromFrequency = 2800)
	{
		_fading = true;
		StartCoroutine(FadeCoroutine(lowPassName, duration, toFrequency, fromFrequency));
	}

	IEnumerator FadeCoroutine(string lowPassName, float duration, float toFrequency, float fromFrequency)
	{
		// _fading = true;
		// float currentTime = 0;

		// while (currentTime <= duration)
		// {
		// 	currentTime += Time.deltaTime;

		//     float frequency = Mathf.SmoothDamp(fromFrequency, toFrequency, ref _yVelocity, _smoothTime);

		//     mixer.SetFloat(lowPassName, frequency);

		// 	yield return null;
		// }

		_frequencyLerped = fromFrequency;

		//Debug.Log("1");
		if (toFrequency < fromFrequency)
		{
			//Debug.Log("2");
			while (_fading)
			{
				//Debug.Log("3");
				if (_frequencyLerped - 1 > toFrequency)
				{
					_frequencyLerped = Mathf.SmoothDamp(_frequencyLerped, toFrequency, ref _yVelocity, duration);
					mixer.SetFloat(lowPassName, _frequencyLerped);
					// print(_frequencyLerped);
				}
				else
				{
					//Debug.Log("5");
					_fading = false;
				}
				yield return null;
			}
		}
		else
		{
			//Debug.Log("6");
			while (_fading)
			{
				//Debug.Log("7");
				//Debug.Log(_frequencyLerped);
				//Debug.Log(toFrequency);
				if (_frequencyLerped + 1 < toFrequency)
				{
					_frequencyLerped = Mathf.SmoothDamp(_frequencyLerped, toFrequency, ref _yVelocity, duration);
					mixer.SetFloat(lowPassName, _frequencyLerped);
					// print(_frequencyLerped);
				}
				else
				{
					_fading = false;
				}
				yield return null;
			}
		}
		_fading = false;
		// print("end");
	}
}
