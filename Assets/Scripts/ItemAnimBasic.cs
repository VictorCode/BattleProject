using UnityEngine;
using System.Collections;

public class ItemAnimBasic : StateMachineBehaviour 
{
	//item animations should be handled differently per item when entering their use-state
	//this is just a functionality example

	private Vector3 pos;
	private Quaternion rot;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		WIHolder whtemp = animator.gameObject.GetComponentInParent<Character>().gameObject.GetComponentInChildren<WIHolder>();
		pos = whtemp.gameObject.transform.localPosition;
		rot = whtemp.gameObject.transform.localRotation;
		animator.GetComponent<IKHands>().ikActive = false;
		whtemp.gameObject.transform.SetParent(animator.GetComponentInChildren<RHandItPos>().gameObject.transform);
		whtemp.gameObject.transform.localPosition = Vector3.zero;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	//OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		WIHolder whtemp = animator.gameObject.GetComponentInParent<Character>().gameObject.GetComponentInChildren<WIHolder>();
	
		whtemp.transform.SetParent(Camera.main.transform);
		whtemp.transform.localRotation = rot;
		whtemp.transform.localPosition = pos;
		animator.GetComponent<IKHands>().ikActive = true;
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
