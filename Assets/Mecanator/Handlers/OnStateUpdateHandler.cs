using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateUpdateHandler : StateMachineHandler
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        var smao = new StateMachineActionObject() { Animator = animator, PathHash = stateInfo.fullPathHash, StateInfo = stateInfo, LayerIndex = layerIndex };
		foreach (var action in Actions)
		{
			action.Execute(smao);
		}
    }
}
