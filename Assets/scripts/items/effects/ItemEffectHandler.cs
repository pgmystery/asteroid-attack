using UnityEngine;

public class ItemEffectHandler : MonoBehaviour
{
    public int price;

    public virtual void DragStarted(GameObject dragObject) {}

    public virtual void DragAborted() {}

    public virtual void DragFinished(GameObject itemDragObject) {}
}
