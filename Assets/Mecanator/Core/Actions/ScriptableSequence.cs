using UnityEngine;
using Unity.Entities;
using DG.Tweening;

public abstract class ScriptableSequence : ScriptableObject
{
    //public abstract Sequence GetSequence(Sequence parentSequence, Entity sequenceSource, Entity sequenceTarget, SequenceOptions options);
    public abstract Sequence GetSequence(Entity entity);
}


[System.Serializable]
public struct SequenceOptions : IComponentData
{
    public float Duration;
    public float Delay;
    public Ease EaseType;
    public bool CompleteOnKill;
}
