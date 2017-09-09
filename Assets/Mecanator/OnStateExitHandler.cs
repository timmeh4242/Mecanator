using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateExitHandler : StateMachineHandler
{
	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateExit (animator, stateInfo, layerIndex);

		var smao = new StateMachineActionObject () { Animator = animator, PathHash = stateInfo.fullPathHash };
		foreach (var action in Actions)
		{
			action.Execute (smao);
		}
	}
}
