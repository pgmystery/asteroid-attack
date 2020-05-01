using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemMenuIndicator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // [SerializeField]
    // private GameObject itemObject;
    [SerializeField]
    private GameObject itemDragObject;
    [SerializeField]
    private Transform contentObject;

    private int createdItems=0;
    public GameObject itemInHand;
    private Rect itemMenuIndicatorRect;
    public bool pointerOverItemMenu=false;

    private TransformHelper transformHelper;
    private IngameHandler ingameHandler;
    // private ItemEffectsHandler itemEffectsHandler;

    public static ItemMenuIndicator itemMenuIndicator;

    void Awake() {
        if (itemMenuIndicator == null) {
            itemMenuIndicator = this;
        }
        else if (itemMenuIndicator != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        // if (itemObject == null) {
        //     Debug.LogError("ItemMenuIndicator: no 'itemObject' set!");
        // }
        if (itemDragObject == null) {
            Debug.LogError("ItemMenuIndicator: no 'itemDragObject' set!");
        }
        if (contentObject == null) {
            Debug.LogError("ItemMenuIndicator: no 'contentObject' set!");
        }

        transformHelper = TransformHelper.transformHelper;
        ingameHandler = IngameHandler.ingameHandler;
        ItemEffectIndicator itemEffectIndicator = ItemEffectIndicator.itemEffectIndicator;

        RectTransform itemMenuIndicatorRectTransform = gameObject.GetComponent<RectTransform>();
        itemMenuIndicatorRect = transformHelper.ConvertRectTransformToRect(itemMenuIndicatorRectTransform);

        // Create some items:
        if (itemEffectIndicator.selectedItems.Count > 0) {
            transform.GetChild(0).gameObject.SetActive(true);
            for (int i=0; i < itemEffectIndicator.selectedItems.Count; i++) {
                if (itemEffectIndicator.selectedItems[i] >= 0) {
                    GameObject itemObject = itemEffectIndicator.itemObjects[itemEffectIndicator.selectedItems[i]];
                    CreateItem(itemObject);
                }
            }
        }
    }

    void Update() {
        // MOUSE INPUT:
        if (itemInHand != null && Input.GetMouseButton(0)) {
            GetInputButton();
        }

        if (Input.GetMouseButtonUp(0)) {
            GetInputButtonUp();
        }

        // TODO: Not really working! Need to cancel the 'OnDrag' function from the item!!!
        if (Input.GetMouseButtonDown(1)) {
            AbortItemInHand();
        }

        // TODO: DON'T NEED THIS????????????????? WHY?????
        // TOUCH INPUT
        // if (Input.touchCount > 0) {
        //     Touch touch = Input.touches[0];
        //     switch (touch.phase) {
        //         case TouchPhase.Moved:
        //             if (itemInHand != null) {
        //                 GetInputButton();
        //             }
        //             break;
        //         case TouchPhase.Ended:
        //             GetInputButtonUp();
        //             break;
        //     }
        // }
    }

    private void GetInputButton() {
        if (ingameHandler.dead) {
            itemInHand.GetComponent<ItemIconDragHandler>().linkedEffectItem.GetComponent<ItemEffectHandler>().DragAborted();
            Destroy(itemInHand);
            itemInHand = null;
        }
        else {
            itemInHand.transform.position = Input.mousePosition;
        }
    }

    private void GetInputButtonUp() {
        if (itemInHand != null) {
            // Check if item overlap with the ScrollView to not trigger the effect
            Rect itemRect = transformHelper.ConvertRectTransformToRect(itemInHand.GetComponent<RectTransform>());
            if (itemRect.Overlaps(itemMenuIndicatorRect)) {
                itemInHand.GetComponent<ItemIconDragHandler>().linkedEffectItem.GetComponent<ItemEffectHandler>().DragAborted();
            }
            else {;
                itemInHand.GetComponent<ItemIconDragHandler>().linkedEffectItem.GetComponent<ItemEffectHandler>().DragFinished(itemInHand);
            }
            Destroy(itemInHand);
            itemInHand = null;
        }
    }

    private void AbortItemInHand() {
        if (itemInHand != null) {
            itemInHand.GetComponent<ItemIconDragHandler>().linkedEffectItem.GetComponent<ItemEffectHandler>().DragAborted();
            Destroy(itemInHand);
            itemInHand = null;
        }
    }

    private GameObject CreateItem(GameObject itemObject) {
        GameObject newItem = Instantiate(itemObject, new Vector3(0, 0, .0f), transform.rotation, contentObject);
        ItemDragHandler newItemDragHandler = newItem.GetComponent<ItemDragHandler>();
        newItemDragHandler.itemMenuIndicator = this;
        // newItem.GetComponent<ItemEffectLoader>().LoadEffectToItem(createdItems);
        // newItem.GetComponent<Image>().sprite = itemEffectsHandler.GetSpriteFromEffect(createdItems);
        createdItems++;
        return newItem;
    }

    public void createItemDragObject(GameObject selectedItem) {
        if (itemInHand == null) {
            itemInHand = Instantiate(itemDragObject, selectedItem.transform.position, transform.rotation, transform.parent);
            itemInHand.GetComponent<ItemIconDragHandler>().linkedEffectItem = selectedItem;
            itemInHand.GetComponent<Image>().sprite = selectedItem.transform.GetChild(0).GetComponent<Image>().sprite;
            selectedItem.GetComponent<ItemEffectHandler>().DragStarted(itemInHand);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        pointerOverItemMenu = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        pointerOverItemMenu = false;
    }
}
