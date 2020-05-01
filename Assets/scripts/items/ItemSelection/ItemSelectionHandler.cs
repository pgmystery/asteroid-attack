using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Data;
using System;

public class ItemSelectionHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject LevelSelectionMenuObject;
    [SerializeField]
    private Button GoToLevelSelectionMenuButton;

    [SerializeField]
    private GameObject allItemListContent;
    [SerializeField]
    private GameObject itemListObject;
    [SerializeField]
    private GameObject itemSelectionObject;
    [SerializeField]
    private GameObject itemSelectionDragObject;

    private GameObject selectedItemInList;
    public GameObject itemInHand;
    public bool handItemHovering=false;
    public List<GameObject> itemSelectionObjects=new List<GameObject>();
    private Rect itemListObjectRect;
    private Rect itemRect;
    private Vector3 hoverInputPosition=Vector3.zero;
    private GameObject latestClosestItemObject;
    private Dictionary<int, GameObject> itemDatas=new Dictionary<int, GameObject>();
    // private DataTable itemsDataTable;

    private ItemEffectIndicator itemEffectIndicator;
    private GameHandler gameHandler;

    public static ItemSelectionHandler itemSelectionHandler;

    void Awake() {
        if (itemSelectionHandler == null) {
            itemSelectionHandler = this;
        }
        else if (itemSelectionHandler != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        if (LevelSelectionMenuObject == null) {
            Debug.Log("ItemSelectionHandler: No 'LevelSelectionMenuObject' set!");
        }
        if (itemListObject == null) {
            Debug.Log("ItemSelectionHandler: No 'itemListObject' set!");
        }
        if (GoToLevelSelectionMenuButton == null) {
            Debug.Log("ItemSelectionHandler: No 'GoToLevelSelectionMenuButton' set!");
        }
        if (allItemListContent == null) {
            Debug.Log("ItemSelectionHandler: No 'allItemListContent' set!");
        }
        if (itemSelectionObject == null) {
            Debug.Log("ItemSelectionHandler: No 'itemSelectionObject' set!");
        }
        if (itemSelectionDragObject == null) {
            Debug.Log("ItemSelectionHandler: No 'itemSelectionDragObject' set!");
        }

        GoToLevelSelectionMenuButton.onClick.AddListener(GoToLevelSelectionMenu);

        gameHandler = GameHandler.game;

        itemEffectIndicator = ItemEffectIndicator.itemEffectIndicator;
        for (int i=0; i < itemEffectIndicator.itemObjects.Length; i++) {
            GameObject itemObject = itemEffectIndicator.itemObjects[i];
            GameObject newItemSelectionObject = Instantiate(itemSelectionObject, Vector3.zero, Quaternion.identity, allItemListContent.transform);
            newItemSelectionObject.GetComponent<ItemSelectionDragHandler>().itemID = i;
            itemDatas.Add(i, newItemSelectionObject);
            newItemSelectionObject.transform.GetChild(1).GetComponent<Image>().sprite = itemObject.transform.GetChild(0).GetComponent<Image>().sprite;
            newItemSelectionObject.transform.GetChild(2).GetComponent<Text>().text = itemObject.GetComponent<ItemEffectHandler>().price.ToString();
            // TODO: CHECK IF ITEM IS USABLE???
            newItemSelectionObject.transform.GetChild(3).gameObject.SetActive(false);
        }

        RectTransform itemListObjectRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        itemListObjectRect = TransformHelper.transformHelper.ConvertRectTransformToRect(itemListObjectRectTransform);

        // Load ItemList
        // itemsDataTable = SQLHandler.sqlHandler.RunCommand("SELECT * FROM selecteditems");
        for (int i=0; i < itemEffectIndicator.selectedItems.Count; i++) {
            Transform content = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
            GameObject newItemInList = Instantiate(itemListObject, content);
            newItemInList.transform.GetChild(1).GetComponent<Text>().text = (i + 1).ToString() + ".";
            newItemInList.GetComponent<ItemListObjectIndicator>().listID = i;
            ItemSelectionHandler.itemSelectionHandler.itemSelectionObjects.Add(newItemInList);
            ItemListObjectIndicator selectedItemListObjectIndicator = newItemInList.GetComponent<ItemListObjectIndicator>();
            if (itemEffectIndicator.selectedItems[i] >= 0) {
                int itemID = itemEffectIndicator.selectedItems[i];
                GameObject item = itemDatas[itemID];
                selectedItemListObjectIndicator.SetItem(itemID, item.transform.GetChild(1).GetComponent<Image>().sprite, item.transform.GetChild(2).GetComponent<Text>().text);
            }
        }
    }

    public void SetItemInHand(GameObject itemObject, int itemID) {
        if (itemInHand == null) {
            SelectItemInList(itemObject);
            itemInHand = Instantiate(itemSelectionDragObject, itemObject.transform.position, itemObject.transform.rotation, transform.parent);
            itemInHand.GetComponent<ItemSelectionDragHandler>().itemID = itemID;
            itemInHand.transform.GetChild(0).GetComponent<Image>().sprite = itemObject.transform.GetChild(1).GetComponent<Image>().sprite;
            itemInHand.transform.GetChild(1).GetComponent<Text>().text = itemObject.transform.GetChild(2).GetComponent<Text>().text;
        }
    }

    void Update() {
        if (itemInHand != null && Input.GetMouseButton(0)) {
            itemInHand.transform.position = Input.mousePosition;
            itemRect = TransformHelper.transformHelper.ConvertRectTransformToRect(itemInHand.GetComponent<RectTransform>());
            if (itemRect.Overlaps(itemListObjectRect)) {
                if (hoverInputPosition == Vector3.zero || Input.mousePosition != hoverInputPosition) {
                    hoverInputPosition = Input.mousePosition;
                    float closestItemDistance = .0f;
                    for (int i=0; i < itemSelectionObjects.Count; i++) {
                        if (latestClosestItemObject == null) {
                            latestClosestItemObject = itemSelectionObjects[i];
                        }
                        closestItemDistance = Vector3.Distance(latestClosestItemObject.transform.position, itemInHand.transform.position);
                        if (closestItemDistance > Vector3.Distance(itemSelectionObjects[i].transform.position, itemInHand.transform.position)) {
                            latestClosestItemObject.GetComponent<ItemListObjectIndicator>().HoverItem(false);
                            latestClosestItemObject = itemSelectionObjects[i];
                            closestItemDistance = Vector3.Distance(latestClosestItemObject.transform.position, itemInHand.transform.position);
                        }
                    }
                    latestClosestItemObject.GetComponent<ItemListObjectIndicator>().HoverItem(true);
                }
            }
            else {
                ResetHovering();
            }
        }

        if (Input.GetMouseButtonUp(0) && itemInHand != null) {
            if (latestClosestItemObject != null) {
                int itemID = itemInHand.GetComponent<ItemSelectionDragHandler>().itemID;
                ItemListObjectIndicator selectedItemListObjectIndicator = latestClosestItemObject.GetComponent<ItemListObjectIndicator>();
                itemEffectIndicator.selectedItems[selectedItemListObjectIndicator.listID] = itemID;
                gameHandler.sql.RunCommand($"UPDATE selectedItems SET itemid={itemID} WHERE id={selectedItemListObjectIndicator.listID + 1}");
                selectedItemListObjectIndicator.SetItem(itemID, itemInHand.transform.GetChild(0).GetComponent<Image>().sprite, itemInHand.transform.GetChild(1).GetComponent<Text>().text);
            }
            ResetHovering();
            Destroy(itemInHand);
            itemInHand = null;
        }

        // TODO: Not really working! Need to cancel the 'OnDrag' function from the item!!!
        if (Input.GetMouseButtonDown(1) && itemInHand != null) {
            ResetHovering();
            Destroy(itemInHand);
            itemInHand = null;
        }
    }

    public void SelectItemInList(GameObject item) {
        if (item != selectedItemInList) {
            if (selectedItemInList != null) {
                selectedItemInList.transform.GetChild(0).gameObject.SetActive(false);
            }
            item.transform.GetChild(0).gameObject.SetActive(true);
            int itemID = item.GetComponent<ItemSelectionDragHandler>().itemID + 1;
            DataTable descriptionTable = gameHandler.sql.RunCommand($"SELECT description from aItems where id={itemID.ToString()}");
            if (descriptionTable.Rows[0]["description"] != DBNull.Value) {
                var itemDescription = descriptionTable.Rows[0]["description"];
                transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>().text = itemDescription.ToString();
            }
            selectedItemInList = item;
        }
    }

    void ResetHovering() {
        if (latestClosestItemObject != null) {
            latestClosestItemObject.GetComponent<ItemListObjectIndicator>().HoverItem(false);
            latestClosestItemObject = null;
            hoverInputPosition = Vector3.zero;
        }
    }

    void GoToLevelSelectionMenu() {
        gameObject.SetActive(false);
        LevelSelectionMenuObject.SetActive(true);
    }
}
