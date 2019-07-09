using UnityEngine;
using UnityEngine.Events;
using System;

public class SetAnimatorIntBehaviour : MonoBehaviour
{
    [SerializeField] private AnimatorIntDelegateTable actions;

    private void Awake()
    {
        foreach (var animatorInt in actions.Keys)
        {
            animatorInt.ParameterHash = Animator.StringToHash(animatorInt.ParameterName);
        }
    }

    public void Execute()
    {
        foreach (var kvp in actions)
        {
            kvp.Value.Invoke(kvp.Key.ParameterHash, kvp.Key.Value);
        }
    }
}

[Serializable]
public class AnimatorSetIntEvent : UnityEvent<int, int> { }

[Serializable]
public class AnimatorInt
{
    public string ParameterName;
    public int Value;
    [HideInInspector] public int ParameterHash;
}

[Serializable]
public class AnimatorIntDelegateTable : SerializableDictionary<AnimatorInt, AnimatorSetIntEvent> { }