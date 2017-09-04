using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animator/Set Animator Paramter")]
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
