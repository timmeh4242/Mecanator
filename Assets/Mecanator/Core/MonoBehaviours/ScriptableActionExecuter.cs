using UnityEngine;
using Unity.Entities;

public class ScriptableActionExecuter : MonoBehaviour
{
    public GameObjectEntity EntityView;

    public void ExecuteAction(AnimationEvent animationEvent)
    {
        var entity = EntityView?.Entity ?? Entity.Null;

        var scriptableAction = animationEvent.objectReferenceParameter as ScriptableAction;
        scriptableAction?.Execute(entity);
    }
}