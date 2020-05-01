using UnityEngine;
using UnityEngine.UI;

public class EarthDamageIndicator : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;

    int healthMax;

    void Start() {
        if (healthBarRect == null) {
            Debug.LogError("STATUS INDICATOR: No healthbar object referenced!");
        }

        if (healthText == null) {
            Debug.LogError("STATUS INDICATOR: No health text object referenced!");
        }

        healthMax = Earth.earth.health;
        healthText.text = healthMax + " HP";
    }

    public void SetHealth(int _cur) {
        float _value = (float)_cur / healthMax;

        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = _cur + " HP";
    }

}
