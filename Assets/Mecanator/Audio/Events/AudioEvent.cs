using Unity.Entities;

public struct AudioEvent : IComponentData
{
    public NativeString64 EventName;
    public AudioOptions Options;
    public Entity Target;
}