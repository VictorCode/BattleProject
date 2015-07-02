using UnityEngine;
using System.Collections;

public class IKHead : MonoBehaviour 
{
	protected Animator animator;
	public bool ikActive = false;
	public Transform lookObj = null;
		
	void Start () 
	{
		animator = GetComponent<Animator>();
		lookObj = GameObject.Find("Center").transform;
		ikActive = true;
	}
	
	void OnAnimatorIK()
	{
		if(animator) 
		{ 
			if(ikActive)
			{
				// Set the look target position, if one has been assigned
				if(lookObj != null)
				{
					animator.SetLookAtWeight(1);
					animator.SetLookAtPosition(lookObj.position);
				}    
			}
		}
		else
		{
			animator.SetLookAtWeight(0);
		}
	}
}
