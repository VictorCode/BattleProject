﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKHands : MonoBehaviour
{
	public Animator animator;
	public bool ikActive = false;
	public Transform rightHandObj = null;
	public Transform leftHandObj = null;
	
	void Start () 
	{
		animator = GetComponent<Animator>();
		ikActive = true;
	}
	
	void OnAnimatorIK()
	{
		if(animator)
		{
			if(ikActive) 
			{	
				// Set the right hand target position and rotation, if one has been assigned
				if(rightHandObj != null) 
				{
					animator.SetIKPositionWeight(AvatarIKGoal.RightHand,1);
					animator.SetIKRotationWeight(AvatarIKGoal.RightHand,1);  
					animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
					animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
				}
				
				// Set the left hand target position and rotation, if one has been assigned
				if(leftHandObj != null) 
				{
					animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
					animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,1);  
					animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHandObj.position);
					animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHandObj.rotation);
				}     
				
			}
			
			//if the IK is not active, set the position and rotation of the hands back where they were
			else
			{
				animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
				animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);
				animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,0);
			}
		}
	}
	
	public void setHands()
	{
		if(GetComponentInParent<Character>().GetComponentInChildren<RHandle>() != null)
		{
			this.rightHandObj = GetComponentInParent<Character>().GetComponentInChildren<RHandle>().gameObject.transform;
		}
		else
		{
			this.rightHandObj = null;
		}
		
		if(this.gameObject.GetComponentInParent<Character>().GetComponentInChildren<LHandle>() != null)
		{
			this.leftHandObj = GetComponentInParent<Character>().GetComponentInChildren<LHandle>().gameObject.transform;
		}
		else
		{
			this.leftHandObj = null;
		}
	}
}
