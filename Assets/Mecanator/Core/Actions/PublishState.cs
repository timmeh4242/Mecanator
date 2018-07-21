#if AlphaECS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AlphaECS;
using Zenject;

public class PublishState : StateMachineAction
{
	protected IEventSystem EventSystem
	{
		get
		{
			if (eventSystem == null)
			{
				eventSystem = ProjectContext.Instance.Container.Resolve<IEventSystem> ();
			}
			return eventSystem;
		}
	}
	private IEventSystem eventSystem;

	public override void Execute (StateMachineActionObject smao)
	{
		base.Execute (smao);

		var animatorStateEvent = new AnimatorStateEvent (smao.Animator, smao.PathHash, smao.StateInfo, smao.LayerIndex, smao.State);
		EventSystem.Publish (animatorStateEvent);
	}
}
#endif
