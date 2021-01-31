using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class LearnedGlyphPreview : MonoBehaviour
{
    private float lerpToTarget;
    private float target;
    private bool show;

    private SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        show = false;
        target = 0;
        lerpToTarget = 0;
    }

    // Update is called once per frame
    void Update()
    {
        target = show ? 0.225f : 0.0f;
        lerpToTarget = Mathf.Lerp(lerpToTarget, target, Time.deltaTime / 2f);
        rend.color = new Color(1, 1, 1, lerpToTarget);
    }

    public void PreviewHieroglyph(HieroGlyph glyph)
    {
        show = true;
        rend.sprite = glyph.talkImage;
        Invoke(nameof(Hide), 3.5f);
    }

    private void Hide()
    {
        show = false;
    }
}