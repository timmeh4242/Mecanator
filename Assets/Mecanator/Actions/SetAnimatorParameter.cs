using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SetAnimatorParameter : StateMachineAction
{
	public string ParameterName;
	public int Value;

//	private int ParameterHash;

//	void Awake()
//	{
//		ParameterHash = Animator.StringToHash (ParameterName);
//	}

	public override void Execute (StateMachineActionObject smao)
	{
		smao.Animator.SetInteger (ParameterName, Value);
	}
}
