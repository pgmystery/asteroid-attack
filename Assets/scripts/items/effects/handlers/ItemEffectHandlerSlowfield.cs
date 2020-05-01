using UnityEngine;
using UnityEngine.UI;

public class ItemEffectHandlerSlowfield : ItemEffectHandler
{
    [SerializeField]
    private GameObject slowfieldObject;

    private IngameHandler ingameHandler;

    void Start() {
        if (price == 0) {
            Debug.LogError("ItemEffectHandlerSlowfield: No price set!");
        }
        if (slowfieldObject == null) {
            Debug.LogError("ItemEffectHandlerSlowfield: No 'slowfieldObject' set!");
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
            GameObject gravitationsfield = Instantiate(slowfieldObject, effectPosition, Quaternion.identity);
        }
    }
}
