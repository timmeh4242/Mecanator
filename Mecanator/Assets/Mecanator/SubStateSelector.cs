using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
//using System;

public class SubStateSelector : StateMachineBehaviour
{
	public int ChildStates = 0;
	public string ParameterName;

	public bool IsRandom = false;

	private int ParamterHash;
	private int State = 0;

	void Awake()
	{
		ParamterHash = Animator.StringToHash (ParameterName);
	}
		
	public override void OnStateMachineEnter (Animator animator, int stateMachinePathHash)
	{
		base.OnStateMachineEnter (animator, stateMachinePathHash);

		if (IsRandom == true)
		{
			State = Random.Range (0, ChildStates);
		}
		else
		{
			State += 1;
			if (State > ChildStates)
			{ State = 0; }
		}

		animator.SetInteger (ParamterHash, State);
	}
}
