using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStateEvent
{
	public Animator Animator;
	public int PathHash;
	public AnimatorStateInfo StateInfo;
	public int LayerIndex;
	public AnimatorState State;

	public AnimatorStateEvent(Animator animator, int pathHash, AnimatorStateInfo stateInfo, int layerIndex, AnimatorState state)
	{
		this.Animator = animator;
		this.PathHash = pathHash;
		this.StateInfo = stateInfo;
		this.LayerIndex = layerIndex;
		this.State = state;
	}
}

public enum AnimatorState
{
	Enter,
	Exit,
	IK,
	Move,
	NormalizedTime,
	Update,
}