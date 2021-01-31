using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    private Vector3 startSize;
    public RectTransform trnsfrm;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        startSize = trnsfrm.localScale;
        target = startSize;
        target = Vector3.zero;
        trnsfrm.localScale = target;
    }

    public void ShowMe()
    {
        target = startSize;
    }

    public void ButtonPressed()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        trnsfrm.localScale = Vector3.Lerp(trnsfrm.localScale, target, Time.deltaTime * 1.5f);
    }
}