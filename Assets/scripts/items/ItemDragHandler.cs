using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler
{
    public ItemMenuIndicator itemMenuIndicator;
    public ItemEffectHandler itemEffectHandler;
    public IngameHandler ingameHandler;

    void OnEnable() {
        IngameHandler.OnGamePointsChanged += GamePointsChanged;
    }

    void OnDisable() {
        IngameHandler.OnGamePointsChanged -= GamePointsChanged;
    }

    void Start() {
        itemMenuIndicator = ItemMenuIndicator.itemMenuIndicator;
        ingameHandler = IngameHandler.ingameHandler;
    }

    private void GamePointsChanged(int gamePoints) {
        if (ingameHandler.gamePoints >= itemEffectHandler.price && transform.GetChild(2).gameObject.activeSelf == true) {
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if(ingameHandler.gamePoints < itemEffectHandler.price && transform.GetChild(2).gameObject.activeSelf == false) {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void OnDrag(PointerEventData eventData) {
        // Debug.Log("Start dragging item");
        if (!itemMenuIndicator.itemInHand && !transform.GetChild(2).gameObject.activeSelf) {
            itemMenuIndicator.createItemDragObject(gameObject);
        }
    }
}
