using Unity.Entities;
using UnityEngine;

public struct AnimatorStateEvent : IComponentData
{
    //public Animator Animator;
    public Entity Entity;
	public int PathHash;
	public AnimatorStateInfo StateInfo;
	public int LayerIndex;
	public AnimatorState State;

	public AnimatorStateEvent(Entity entity, int pathHash, AnimatorStateInfo stateInfo, int layerIndex, AnimatorState state)
	{
		this.Entity = entity;
		this.PathHash = pathHash;
		this.StateInfo = stateInfo;
		this.LayerIndex = layerIndex;
		this.State = state;
	}
}

public enum AnimatorState
{
	Enter,
	Exit,
	IK,
	Move,
	NormalizedTime,
	Update,
}