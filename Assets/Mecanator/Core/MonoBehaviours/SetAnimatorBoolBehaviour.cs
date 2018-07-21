using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class SetAnimatorBoolBehaviour : MonoBehaviour
{
    [SerializeField] private AnimatorBoolDelegateTable actions;

    private void Awake()
    {
        foreach (var animatorBool in actions.Keys)
        {
            animatorBool.ParameterHash = Animator.StringToHash(animatorBool.ParameterName);
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
public class AnimatorSetBoolEvent : UnityEvent<int, bool> { }

[Serializable]
public class AnimatorBool
{
    public string ParameterName;
    public bool Value;
    [HideInInspector] public int ParameterHash;
}

[Serializable]
public class AnimatorBoolDelegateTable : SerializableDictionary<AnimatorBool, AnimatorSetBoolEvent> { }