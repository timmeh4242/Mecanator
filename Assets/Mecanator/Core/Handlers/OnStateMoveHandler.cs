using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateMoveHandler : StateMachineHandler
{
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateMove(animator, stateInfo, layerIndex);

        if (animator.GetLayerWeight (layerIndex) <= 0f && layerIndex > 0 && !IgnoreWeight) { return; }

        var smao = new StateMachineActionObject() { Animator = animator, PathHash = stateInfo.fullPathHash, StateInfo = stateInfo, LayerIndex = layerIndex, State = AnimatorState.Move };
		foreach (var action in Actions)
		{
			action.Execute(smao);
		}
    }
}
