using Unity.Entities;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayAudio : StateMachineAction
{
	public string EventName;
	NativeString64 eventName;
	public AudioOptions Options;

	AudioBehaviour audioBehaviour;

    EntityEventSystem eventSystem;
    EntityEventSystem EventSystem => eventSystem ?? (eventSystem = World.Active.GetOrCreateSystem<EntityEventSystem>());

    public override void Execute (StateMachineActionObject smao)
	{
        audioBehaviour = smao.Animator.GetComponent<AudioBehaviour>();
        if (audioBehaviour != null)
        {
            eventName = new NativeString64(audioBehaviour.AudioPrefix + EventName + audioBehaviour.AudioSuffix);
        }
        else
        {
            eventName = new NativeString64(EventName);
        }
        var gao = smao.Animator.GetComponent<GameObjectEntity>();
        var target = gao != null ? gao.Entity : Entity.Null;
		EventSystem.PublishData (new AudioEvent () { EventName = eventName, Options = Options, Target = target });
	}
}