using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateEnterHandler : StateMachineHandler
{
	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter (animator, stateInfo, layerIndex);

		if (animator.GetLayerWeight (layerIndex) <= 0f && layerIndex > 0 && !IgnoreWeight) { return; }

		var smao = new StateMachineActionObject() { Animator = animator, PathHash = stateInfo.fullPathHash, StateInfo = stateInfo, LayerIndex = layerIndex, State = AnimatorState.Enter };
		foreach (var action in Actions)
		{
			action.Execute (smao);
		}
	}
}