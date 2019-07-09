using Unity.Entities;
using UnityEngine;

public class AudioBehaviour : MonoBehaviour
{
    public string AudioPrefix;
    public string AudioSuffix;

    public GameObjectEntity GameObjectEntity;

    EntityEventSystem eventSystem;
    EntityEventSystem EventSystem => eventSystem ?? (eventSystem = World.Active.GetOrCreateSystem<EntityEventSystem>());

    public void PlayAudio(AnimationEvent animationEvent)
    {
        EventSystem.PublishData(new AudioEvent()
        {
            EventName = new NativeString64(AudioPrefix + animationEvent.stringParameter + AudioSuffix),
            Options = animationEvent.objectReferenceParameter as AudioOptionsProxy != null ? (animationEvent.objectReferenceParameter as AudioOptionsProxy).AudioOptions : new AudioOptions(),
            Target = GameObjectEntity.Entity,
        });
    }
}