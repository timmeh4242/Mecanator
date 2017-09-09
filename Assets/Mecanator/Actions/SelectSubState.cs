using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectSubState : StateMachineAction
{
	public int ChildStates = 0;
	public string ParameterName;

	public bool IsRandom = false;

//	private int ParameterHash;
	private int State = 0;

//	void Awake()
//	{
//		ParameterHash = Animator.StringToHash (ParameterName);
//	}

	public override void Execute (StateMachineActionObject smao)
	{
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

		smao.Animator.SetInteger (ParameterName, State);
	}
}
