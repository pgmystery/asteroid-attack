using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSelectionDragHandler : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public int itemID;

    private ItemSelectionHandler itemSelectionHandler;

    void Start() {
        itemSelectionHandler = ItemSelectionHandler.itemSelectionHandler;
    }

    public void OnDrag(PointerEventData eventData) {
        if (itemSelectionHandler.itemInHand == null && !transform.GetChild(3).gameObject.activeSelf) {
            itemSelectionHandler.SetItemInHand(gameObject, itemID);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        itemSelectionHandler.SelectItemInList(gameObject);
    }
}
