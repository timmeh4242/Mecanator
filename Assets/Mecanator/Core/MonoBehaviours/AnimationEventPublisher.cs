using Unity.Entities;
using UnityEngine;

public class AnimationEventPublisher : MonoBehaviour
{
    EntityEventSystem eventSystem;
    EntityEventSystem EventSystem => eventSystem ?? (eventSystem = World.Active.GetOrCreateSystem<EntityEventSystem>());

    public GameObjectEntity EntityView;

    public void PublishEvent(AnimationEvent animationEvent)
    {
        var entity = EntityView?.Entity ?? Entity.Null;
        EventSystem.PublishData (new AnimationEventData(entity, animationEvent.stringParameter, animationEvent.floatParameter, animationEvent.intParameter));
    }
}
