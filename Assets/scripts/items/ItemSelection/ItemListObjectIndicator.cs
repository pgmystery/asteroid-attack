using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// public class ItemListObjectIndicator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
public class ItemListObjectIndicator : MonoBehaviour
{
    [SerializeField]
    private Color32 normalColor;
    [SerializeField]
    private Color32 hoverColor;

    public int listID;
    public int selectedIconID=0;

    private Image itemBackgroundImage;

    private ItemSelectionHandler itemSelectionHandler;

    void Start() {
        itemSelectionHandler = ItemSelectionHandler.itemSelectionHandler;

        itemBackgroundImage = transform.GetChild(0).GetComponent<Image>();
        itemBackgroundImage.color = normalColor;
    }

    public void HoverItem(bool status) {
        if (status) {
            itemBackgroundImage.color = hoverColor;
        }
        else {
            itemBackgroundImage.color = normalColor;
        }
    }

    public void SetItem(int iconID, Sprite icon, string price) {
        selectedIconID = iconID;
        Transform iconObject = transform.GetChild(2);
        iconObject.GetComponent<Image>().sprite = icon;
        iconObject.GetChild(0).GetComponent<Text>().text = price;
    }
}
