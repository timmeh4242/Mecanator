using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStateMachineEnterHandler : StateMachineBehaviour
{
	public StateMachineAction[] Actions;

	public override void OnStateMachineEnter (Animator animator, int stateMachinePathHash)
	{
		base.OnStateMachineEnter (animator, stateMachinePathHash);

		var smao = new StateMachineActionObject () { Animator = animator, PathHash = stateMachinePathHash };
		foreach (var action in Actions)
		{
			action.Execute (smao);
		}
	}
}
