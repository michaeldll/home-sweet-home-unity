using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DistanceDissolveTarget : MonoBehaviour
{
	[SerializeField] private Transform targetTransform;
	private Material _materialRef;
	private Renderer _renderer;
	public Renderer Renderer
	{
		get
		{
			if (_renderer == null)
				_renderer = this.GetComponent<Renderer>();

			return _renderer;
		}
	}
	public Material MaterialRef
	{
		get
		{
			if (_materialRef == null)
				_materialRef = Renderer.material;
			return _materialRef;
		}
	}
	private void Awake()
	{
		_renderer = this.GetComponent<Renderer>();
		_materialRef = _renderer.material;
	}

	private void Update()
	{
		if (targetTransform != null)
		{
			MaterialRef.SetVector("_Position", targetTransform.position);
		}
	}

	private void OnDestroy()
	{
		_renderer = null;
		if (_materialRef != null)
			Destroy(_materialRef);
		_materialRef = null;
	}
}
