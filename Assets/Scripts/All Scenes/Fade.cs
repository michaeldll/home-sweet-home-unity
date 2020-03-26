using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
	[SerializeField] public Image background;

	public void FadeOut()
	{
		background.enabled = true;
		background.canvasRenderer.SetAlpha(0.0f);
		background.CrossFadeAlpha(1, 2, false);
	}

	public void FadeIn()
	{
		background.enabled = true;
		background.canvasRenderer.SetAlpha(1.0f);
		background.CrossFadeAlpha(0, 3, false);
	}
}
