#if AlphaECS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AlphaECS;
using Zenject;

public class PublishEvent : StateMachineAction
{
	public string EventName = "";

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

		var type = Type.GetType (EventName);
		var instance = Activator.CreateInstance (type);
		EventSystem.Publish (instance);
	}
}
#endif
