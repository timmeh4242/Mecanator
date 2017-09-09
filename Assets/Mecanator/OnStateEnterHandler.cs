using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateEnterHandler : StateMachineHandler
{
	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter (animator, stateInfo, layerIndex);

		var smao = new StateMachineActionObject () { Animator = animator, PathHash = stateInfo.fullPathHash };
		foreach (var action in Actions)
		{
			action.Execute (smao);
		}
	}
}
