using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MakeSizeAccordingToText : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    private RectTransform _transform;

    public float padding;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 preferredValues = textComponent.GetPreferredValues();
        _transform = (RectTransform) transform;
        Rect transformRect = _transform.rect;
        transformRect.width = preferredValues.x;
        transformRect.height = preferredValues.y;
        _transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,transformRect.width + padding);
        _transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,transformRect.height+ padding);
    }
}
