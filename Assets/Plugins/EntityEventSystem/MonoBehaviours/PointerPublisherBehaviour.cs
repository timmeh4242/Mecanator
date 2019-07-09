using Unity.Entities;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerPublisherBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IDragHandler
{
    public GameObjectEntity gameObjectEntity;

    EntityEventSystem eventSystem;
    EntityEventSystem EventSystem => eventSystem ?? (eventSystem = World.Active.GetExistingSystem<EntityEventSystem>());

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.PublishData(new PointerEvent(gameObjectEntity.Entity, PointerEventType.Enter));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.PublishData(new PointerEvent(gameObjectEntity.Entity, PointerEventType.Exit));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventSystem.PublishData(new PointerEvent(gameObjectEntity.Entity, PointerEventType.Down));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EventSystem.PublishData(new PointerEvent(gameObjectEntity.Entity, PointerEventType.Up));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventSystem.PublishData(new PointerEvent(gameObjectEntity.Entity, PointerEventType.Click));
    }

    public void OnDrag(PointerEventData eventData)
    {
        EventSystem.PublishData(new PointerEvent(gameObjectEntity.Entity, PointerEventType.Drag));
    }
}

public struct PointerEvent : IComponentData
{
    public Entity Entity;
    public PointerEventType EventType;

    public PointerEvent(Entity entity, PointerEventType eventType)
    {
        Entity = entity;
        EventType = eventType;
    }
}

public struct EventDataBase : IComponentData { }

public enum PointerEventType
{
    Enter,
    Exit,
    Down,
    Up,
    Click,
    Drag,
}
