using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTwoManager : MonoBehaviour
{
	// Declare public variables to be accessed throughout the scene
	public GameObject GCTextToSpeech;
	public static SceneTwoManager Instance { get; private set; } // static singleton
	void Awake()
	{
		if (Instance == null) { Instance = this; }
		else { Destroy(gameObject); }
		// Cache references to all desired variables
		GCTextToSpeech = GameObject.Find("GCTextToSpeech");
	}
}