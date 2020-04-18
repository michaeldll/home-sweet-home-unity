using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMasturbationAnimation : MonoBehaviour
{
	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();

		if (GameManager.gender == "male") anim.SetBool("isMale", true);
		else anim.SetBool("isMale", false);
		//set masturbation animation
		// if (GameManager.gender == "male") anim.clip ;
		// else anim.clip = femaleClip;
	}

}

