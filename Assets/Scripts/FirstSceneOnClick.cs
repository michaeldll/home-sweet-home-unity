using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstSceneOnClick : MonoBehaviour
{
	[SerializeField] private Camera cam = null;
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				Transform objectHit = hit.transform;
				Debug.Log(objectHit);
				StartCoroutine(ScaleMe(objectHit));
			}
		}


	}
	IEnumerator ScaleMe(Transform objTr)
	{
		objTr.DOScale(1.1f, 0.2f);
		yield return new WaitForSeconds(0.2f);
		objTr.DOScale(1.0f, 0.2f);
		yield return new WaitForSeconds(0.2f);
		SceneTransitioner.Instance.LoadScene(1);
	}
}