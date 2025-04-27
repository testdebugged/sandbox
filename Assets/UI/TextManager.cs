using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    // Should be used with TextMeshPro
    TextMeshProUGUI text_m;
    //TextMeshPro _self;
    float timeElasped = 0.0f;
    public float delay = 0.0f;
    bool shouldHide = false;
    public bool startHidden = true;
    public bool overrideDelay = true;
    public TextManager? pair;
    void Start()
    {
        text_m = GetComponent<TextMeshProUGUI>();
        visible(!startHidden, 0.0f);
        //_self = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElasped += Time.deltaTime;
        if (shouldHide && delay < timeElasped)
        {
            text_m.enabled = false;
        }
        else
        {
            text_m.enabled = true;
        }
    }

    public void visible(bool t, float d = 0.0f)
    {
        timeElasped = 0f;
        if (overrideDelay)
        {
            delay = d;
        }
        shouldHide = !t;
        if (pair != null)
        {
            pair.visible(t, d);
        }
    }

    public void write(string _text)
    {
        text_m.text = _text;
        if (pair != null)
        {
            pair.write(_text);
        }
    }


}
