using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DelegateAction : ScriptableObject
{
	public abstract void Execute (object data);
}
