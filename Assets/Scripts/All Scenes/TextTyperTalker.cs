using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedBlueGames.Tools.TextTyper;
// using TMPro;

public class TextTyperTalker : MonoBehaviour
{
	[SerializeField] private string text = GameManager.textOne;
	[SerializeField] private float delay = 0.064f;
	[SerializeField] private TextTyper textTyperComponent;

	void Start()
	{
		text = GameManager.textOne;
		textTyperComponent.TypeText(text, delay);
	}
}
