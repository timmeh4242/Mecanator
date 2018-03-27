using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateNormalizedTimeHandler : StateMachineHandler
{
    [SerializeField]
	private float NormalizedTime;
    [SerializeField]
    private bool IsLooped;

	private bool emit = false;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

		if (animator.GetLayerWeight (layerIndex) <= 0f && layerIndex > 0) { return; }

        var normalizedTime = IsLooped ? stateInfo.normalizedTime % 1 : stateInfo.normalizedTime;
        if (emit)
		{
			if (normalizedTime >= NormalizedTime)
			{
				emit = false;

				var smao = new StateMachineActionObject() { Animator = animator, PathHash = stateInfo.fullPathHash, StateInfo = stateInfo, LayerIndex = layerIndex, State = AnimatorState.NormalizedTime };
				foreach (var action in Actions)
				{
					action.Execute(smao);
				}
			}
		}
		else
		{
			if (normalizedTime < NormalizedTime)
			{
				emit = true;
			}
		}
    }
}
