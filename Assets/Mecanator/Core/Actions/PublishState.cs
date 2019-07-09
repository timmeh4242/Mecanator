using Unity.Entities;

public class PublishState : StateMachineAction
{
    EntityEventSystem eventSystem;
    EntityEventSystem EventSystem => eventSystem ?? (eventSystem = World.Active.GetOrCreateSystem<EntityEventSystem>());

    public override void Execute (StateMachineActionObject smao)
	{
		base.Execute (smao);

        var gao = smao.Animator.GetComponent<GameObjectEntity>();
        var target = gao != null ? gao.Entity : Entity.Null;
        var animatorStateEvent = new AnimatorStateEvent (target, smao.PathHash, smao.StateInfo, smao.LayerIndex, smao.State);
		EventSystem.PublishData (animatorStateEvent);
	}
}