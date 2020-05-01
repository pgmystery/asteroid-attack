using UnityEngine;
using UnityEngine.UI;

public class ItemEffectHandlerSimplerocket : ItemEffectHandler
{
    [SerializeField]
    private GameObject simplerocketObject;
    [SerializeField]
    private GameObject dottedLineObject;

    private GameObject itemInHand;
    private GameObject dottedLineObjectClone;
    private SimplerocketDottetLine dottetlineScript;

    private IngameHandler ingameHandler;
    private GUIOverlayScript guiOverlayScript;

    void Start() {
        if (price == 0) {
            Debug.LogError("ItemEffectHandlerSimplerocket: No price set!");
        }
        if (simplerocketObject == null) {
            Debug.LogError("ItemEffectHandlerSimplerocket: No 'simplerocketObject' set!");
        }
        if (dottedLineObject == null) {
            Debug.LogError("ItemEffectHandlerSimplerocket: No 'dottedLineObject' set!");
        }

        transform.GetChild(1).GetComponent<Text>().text = price.ToString();

        gameObject.GetComponent<ItemDragHandler>().itemEffectHandler = this;

        ingameHandler = IngameHandler.ingameHandler;
        guiOverlayScript = GUIOverlayScript.guiOverlayScript;
    }

    public override void DragStarted(GameObject dragObject) {
        itemInHand = dragObject;
        dottedLineObjectClone = Instantiate(dottedLineObject, guiOverlayScript.transform);
        dottetlineScript = dottedLineObjectClone.GetComponent<SimplerocketDottetLine>();
    }

    public override void DragAborted() {
        Destroy(dottedLineObjectClone);
    }

    // void FixUpdate() {
    void Update() {
        // TODO: Check if the Framerate is still good ?!
        if (dottedLineObjectClone != null) {
            dottetlineScript.targetPosition = Camera.main.ScreenToWorldPoint(itemInHand.transform.position);
        }
    }

    public override void DragFinished(GameObject itemDragObject) {
        if (ingameHandler.gamePoints >= price) {
            ingameHandler.RemovePoints(price);
            Vector3 newPosition = Vector3.back;
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(itemInHand.transform.position);
            Vector3 difference = targetPosition - newPosition;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            GameObject rocketObject = Instantiate(simplerocketObject, newPosition, Quaternion.Euler(.0f, .0f, rotationZ - 90f));
            Destroy(dottedLineObjectClone);
            // rocketObject.GetComponent<ItemEffectSimplerocket>().StartMoving((targetPosition - rocketObject.transform.position).normalized);
            rocketObject.GetComponent<ItemEffectSimplerocket>().StartMoving(targetPosition);
        }
    }
}
