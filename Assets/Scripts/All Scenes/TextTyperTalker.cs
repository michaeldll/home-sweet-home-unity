using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedBlueGames.Tools.TextTyper;
// using TMPro;

public class TextTyperTalker : MonoBehaviour
{
	public string text = "default text please change";
	[SerializeField] private float delay = 0.064f;
	[SerializeField] private TextTyper textTyperComponent;

	void Start()
	{
		textTyperComponent.enabled = true;
		textTyperComponent.TypeText(text, delay);
	}
}
