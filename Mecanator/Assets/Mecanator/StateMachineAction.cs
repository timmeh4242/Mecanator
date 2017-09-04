using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineAction : ScriptableObject
{
	public abstract void Execute (StateMachineActionObject stateMachineActionObject);
}

public class StateMachineActionObject
{
	public Animator Animator;
	public AnimatorStateInfo StateInfo;
	public int LayerIndex;
	public int PathHash;
}
