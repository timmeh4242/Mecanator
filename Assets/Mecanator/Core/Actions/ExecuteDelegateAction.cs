using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class ExecuteDelegateAction : StateMachineAction
{
	public DelegateAction DelegateAction;

	public override void Execute (StateMachineActionObject smao)
	{
		base.Execute (smao);

		DelegateAction.Execute (smao);
	}
}
