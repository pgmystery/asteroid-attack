using UnityEngine;
// using UnityEngine.EventSystems;

public class TransformHelper : MonoBehaviour
{
    public static TransformHelper transformHelper;

    void Awake() {
        if (transformHelper == null) {
            transformHelper = this;
        }
        else if (transformHelper != this) {
            Destroy(gameObject);
        }
    }

    public Rect ConvertRectTransformToRect(RectTransform rectTransform) {
        return new Rect(rectTransform.worldToLocalMatrix.GetColumn(3).x - (rectTransform.rect.width / 2), rectTransform.worldToLocalMatrix.GetColumn(3).y - (rectTransform.rect.height / 2), rectTransform.rect.width, rectTransform.rect.height);
    }
}
