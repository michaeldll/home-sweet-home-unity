using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DitzelGames.FastIK;

public class NewGyroRotate : MonoBehaviour
{
	[SerializeField] private Transform phone = null;
	[SerializeField] private float speed = 1.0f;
	[SerializeField] private float _xLowerLimit = -0.49f, _xUpperLimit = 0.02f;
	[SerializeField] private float _yLowerLimit = 1.2f, _yUpperLimit = 1.42f;
	[SerializeField] private Animator turnBackController;
	[SerializeField] private FastIKLook lookPhone;

	private float _clampedX = 0;
	private float _clampedY = 0;
	private bool _isTurning = false;
	private bool _facingBackwards = false;
	private Vector3 _gyroPos;

	void Update()
	{
		Debug.Log(handleGyroX(GameManager.gyroAngleX));
		Debug.Log(GameManager.gyroAngleX);

		if(!_facingBackwards){
			// Handle and clamp gyro coordinates
			_clampedX = Mathf.Clamp(handleGyroX(GameManager.gyroAngleX), _xLowerLimit, _xUpperLimit);
			_clampedY = Mathf.Clamp(handleGyroY(GameManager.gyroAngleZ), _yLowerLimit, _yUpperLimit);

			_gyroPos.x = _clampedX;
			_gyroPos.y = _clampedY;
			_gyroPos.z = phone.localPosition.z;

			phone.localPosition = Vector3.Lerp(phone.localPosition, _gyroPos, Time.deltaTime * speed) ;

			//turn back
			if (!GameManager.isIntro && handleGyroX(GameManager.gyroAngleX) > _xUpperLimit && !_isTurning || handleGyroX(GameManager.gyroAngleX) < _xLowerLimit && !_isTurning)
			{
				_isTurning = true;
				_facingBackwards = true;
				ToggleLookPhone();
				Turn();
				Invoke("ResetIsTurning", 5f);
				Debug.Log("turn");
			}
		}else if (_facingBackwards){
			//turn front
			if (!GameManager.isIntro && GameManager.gyroAngleX < 0 && GameManager.gyroAngleX > -180 && !_isTurning)
			{
				_isTurning = true;
				_facingBackwards = false;
				Invoke("ToggleLookPhone", 4f);
				Turn();
				Invoke("ResetIsTurning", 5f);
				Debug.Log("turn");
			}
		}

	}

	float handleGyroX(float x)
	{
		return Map(0, -180, _xLowerLimit, _xUpperLimit, x);
	}

	float handleGyroY(float y)
	{
		return Map(0, 180, _yLowerLimit, _yUpperLimit, y);
	}

	public float Map(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue){
	
		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
	
		return(NewValue);
	}

	public void ResetIsTurning(){
		_isTurning = false;
	}

	public void ToggleLookPhone(){
		lookPhone.enabled = !lookPhone.enabled;
	}

	public void Turn(){
		if(turnBackController.GetBool("turnBack")){
			turnBackController.SetBool("turnFront", !turnBackController.GetBool("turnFront"));
		}
		else if(!turnBackController.GetBool("turnBack")){
			turnBackController.SetBool("turnBack", true);
		}
	}
}