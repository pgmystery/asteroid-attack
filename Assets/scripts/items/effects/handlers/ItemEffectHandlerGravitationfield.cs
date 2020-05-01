using UnityEngine;
using UnityEngine.UI;

public class ItemEffectHandlerGravitationfield : ItemEffectHandler
{
    [SerializeField]
    private GameObject gravitationsfieldObject;

    private IngameHandler ingameHandler;

    void Start() {
        if (price == 0) {
            Debug.LogError("ItemEffectHandlerGravitationfield: No price set!");
        }
        if (gravitationsfieldObject == null) {
            Debug.LogError("ItemEffectHandlerGravitationfield: No 'gravitationsfieldObject' set!");
        }

        transform.GetChild(1).GetComponent<Text>().text = price.ToString();

        gameObject.GetComponent<ItemDragHandler>().itemEffectHandler = this;

        ingameHandler = IngameHandler.ingameHandler;
    }

    // public override void DragStarted() {
    //     Debug.Log("DRAG STARDED!");
    // }

    // public override void DragAborted() {
    //     Debug.Log("DRAG ABORTED!");
    // }

    public override void DragFinished(GameObject itemDragObject) {
        if (ingameHandler.gamePoints >= price) {
            ingameHandler.RemovePoints(price);
            Vector3 effectPosition = Camera.main.ScreenToWorldPoint(itemDragObject.transform.position);
            effectPosition.z = 1;
            GameObject gravitationsfield = Instantiate(gravitationsfieldObject, effectPosition, Quaternion.identity);
        }
    }
}
