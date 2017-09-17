using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateIKHandler : StateMachineHandler
{
    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateIK(animator, stateInfo, layerIndex);

		var smao = new StateMachineActionObject() { Animator = animator, PathHash = stateInfo.fullPathHash, StateInfo = stateInfo, LayerIndex = layerIndex };
		foreach (var action in Actions)
		{
			action.Execute(smao);
		}
    }
}
