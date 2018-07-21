using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateIKHandler : StateMachineHandler
{
    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateIK(animator, stateInfo, layerIndex);

        if (animator.GetLayerWeight (layerIndex) <= 0f && layerIndex > 0 && !IgnoreWeight) { return; }

        var smao = new StateMachineActionObject() { Animator = animator, PathHash = stateInfo.fullPathHash, StateInfo = stateInfo, LayerIndex = layerIndex, State = AnimatorState.IK };
		foreach (var action in Actions)
		{
			action.Execute(smao);
		}
    }
}
