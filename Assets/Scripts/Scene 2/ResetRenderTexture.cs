using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetRenderTexture : MonoBehaviour
{
	[SerializeField] private RenderTexture renderTexture;

	void Start()
	{
		renderTexture.Release();
	}
}