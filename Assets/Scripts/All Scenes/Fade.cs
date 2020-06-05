using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
	[SerializeField] public Image img;

	public void FadeOut(float duration = 2.0f)
	{
		img.enabled = true;
		img.canvasRenderer.SetAlpha(0.0f);
		img.CrossFadeAlpha(1, duration, false);
	}

	public void FadeIn()
	{
		img.enabled = true;
		img.canvasRenderer.SetAlpha(1.0f);
		img.CrossFadeAlpha(0, 3, false);
	}
}
