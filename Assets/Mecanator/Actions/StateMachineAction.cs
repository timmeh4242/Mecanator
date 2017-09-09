using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineAction : ScriptableObject
{
	public abstract void Execute (StateMachineActionObject stateMachineActionObject);

    private void OnEnable()
    {
		hideFlags = HideFlags.HideAndDontSave;
	}
}
