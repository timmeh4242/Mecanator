#if AlphaECS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlphaECS;
using Zenject;

[System.Serializable]
public class PlayAudio : StateMachineAction
{
	public string EventName;
	private string eventName;
	public AudioOptions Options;

	private AudioBehaviour audioBehaviour;

	private static IEventSystem EventSystem
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
	private static IEventSystem eventSystem;

    public override void Execute (StateMachineActionObject smao)
	{
        audioBehaviour = smao.Animator.GetComponent<AudioBehaviour>();
        if (audioBehaviour != null)
        {
            eventName = audioBehaviour.AudioPrefix + EventName + audioBehaviour.AudioSuffix;
        }
        else
        {
            eventName = EventName;
        }
		EventSystem.Publish (new AudioEvent () { EventName = eventName, Options = Options, Target = smao.Animator });
	}
}
#endif