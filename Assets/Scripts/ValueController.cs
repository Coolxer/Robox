using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueController : MonoBehaviour
{
    private Slider slider;
    private Text text;
    private float lastValue = 0.0001f;
    private float delta = 0;

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        text = transform.Find("value").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(
            delegate { setValue(slider.value);}
        );
    }

    public float getValue()
    {
        return slider.value;
    }

    public float getLastValue()
    {
        return lastValue;
    }

    public float getDelta()
    {
        return delta;
    }

    public void resetDelta()
    {
        delta = 0;
    }

    public float getMin()
    {
        return slider.minValue;
    }

    public float getMax()
    {
        return slider.maxValue;
    }

    public void setValue(float value)
    {
        slider.value = value;
        text.text = value.ToString();

        delta = value - lastValue;
        lastValue = value;
    }
}

