//using UnityEngine;
//using Unity.Entities;
//using System;


////TODO -> add support for building out list of IComponentDatas from inspector...
////...see EntityBehaviourEditor for reference???
//public class PublishEvent : StateMachineAction
//{
//    public string EventName = "";

//    EntityEventSystem eventSystem;
//    EntityEventSystem EventSystem => eventSystem ?? (eventSystem = World.Active.GetOrCreateSystem<EntityEventSystem>());

//    public override void Execute(StateMachineActionObject smao)
//    {
//        base.Execute(smao);

//        var type = Type.GetType(EventName);
//        if (!type.IsValueType || !typeof(IComponentData).IsAssignableFrom(type))
//        {
//            Debug.LogWarning("You can only publish IComponentData");
//        }
//        else
//        {
//            var instance = (IComponentData)Activator.CreateInstance(type);
//            EventSystem.PublishData(instance);
//        }
//    }
//}
