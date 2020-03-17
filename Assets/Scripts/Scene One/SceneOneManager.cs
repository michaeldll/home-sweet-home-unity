using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOneManager : MonoBehaviour
{
	private bool _isBeginning = false;

	void Update()
	{
		if (GameManager.isPhoneConnected && !_isBeginning) BeginGame();
	}

	private void BeginGame()
	{
		_isBeginning = true;
		SceneTransitioner.Instance.LoadScene(1);
	}
}
