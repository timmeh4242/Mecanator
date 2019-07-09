using UnityEngine;
using Unity.Entities;
using DG.Tweening;

public class ScriptableSequenceExecuter : MonoBehaviour
{
    public GameObjectEntity EntityView;

    Sequence sequence;

    public bool IsEnabled;

    public void ExecuteSequence(AnimationEvent animationEvent)
    {
        if (!IsEnabled)
            return;

        var entity = EntityView?.Entity ?? Entity.Null;

        var scriptableSequence = animationEvent.objectReferenceParameter as ScriptableSequence;
        scriptableSequence?.GetSequence(entity);
    }
}
