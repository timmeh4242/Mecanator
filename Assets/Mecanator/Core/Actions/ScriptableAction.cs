using UnityEngine;

public abstract class ScriptableAction : ScriptableObject
{
	public abstract void Execute<T>(T data);
}
