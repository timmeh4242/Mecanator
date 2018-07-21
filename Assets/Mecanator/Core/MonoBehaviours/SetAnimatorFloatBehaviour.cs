using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class SetAnimatorFloatBehaviour : MonoBehaviour
{
    [SerializeField] private AnimatorFloatDelegateTable actions;

    private void Awake()
    {
        foreach (var animatorFloat in actions.Keys)
        {
            animatorFloat.ParameterHash = Animator.StringToHash(animatorFloat.ParameterName);
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
public class AnimatorSetFloatEvent : UnityEvent<int, float> { }

[Serializable]
public class AnimatorFloat
{
    public string ParameterName;
    public float Value;
    [HideInInspector] public int ParameterHash;
}

[Serializable]
public class AnimatorFloatDelegateTable : SerializableDictionary<AnimatorFloat, AnimatorSetFloatEvent> { }