using Unity.Entities;

public struct AnimationEventData : IComponentData
{
    public Entity Entity;
    public NativeString64 StringParameter;
    public float FloatParameter;
    public int IntParameter;

    public AnimationEventData(Entity entity, string stringParameter, float floatParameter, int intParameter)
    {
        Entity = entity;
        StringParameter = new NativeString64(stringParameter);
        FloatParameter = floatParameter;
        IntParameter = intParameter;
    }
}

//public class AnimationEventProxy
//{
//	public Animator Animator;
//	public AnimationEvent AnimationEvent;

//	public AnimationEventProxy(Animator animator, AnimationEvent animationEvent)
//	{
//		this.Animator = animator;
//		this.AnimationEvent = animationEvent;
//	}
//}


