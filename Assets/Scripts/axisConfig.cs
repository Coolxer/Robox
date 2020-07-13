using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class axisConfig : MonoBehaviour
{
    public GameObject axis;

    public Vector3 vector;
    public float min;
    public float max;
    private float speed = 50.0f;

    private float lastSliderValue;

    private bool auto = false;

    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        sliderController.minValue = min;
        sliderController.maxValue = max;

         sliderController.onValueChanged.AddListener(
             delegate { onSlideRotate();}
        );

        speedController.minValue = 0.0f;
        speedController.value = this.speed;
        speedController.maxValue = 100.0f;

        speedController.onValueChanged.AddListener(
             delegate { setSpeed();}
        );

        speedText.text = this.speed.ToString();
       
        autoButton.onClick.AddListener(setAutoRotate);

        lastSliderValue = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!auto) return;

        float newValue = lastSliderValue + (this.speed * Time.deltaTime * direction);
        float delta = newValue - lastSliderValue;
        transform.Rotate(vector * delta);

        sliderController.value = newValue;

        if(newValue >= max && direction == 1) direction = -1;
        else if(newValue <= min && direction == -1) direction = 1;

        posText.text = newValue.ToString();

        lastSliderValue = newValue;
    }

    public void onSlideRotate()
    {
        float currentValue = sliderController.value;

        float delta = currentValue - lastSliderValue;

        transform.Rotate (vector * delta);

        posText.text = currentValue.ToString();

        lastSliderValue = currentValue;
    }

    public void setAutoRotate()
    {
        string text = auto ? "AUTO" : "STOP";

        autoButton.GetComponentInChildren<Text>().text = text;

        auto = !auto;
        
        lastSliderValue = lastSliderValue == 0 ? 0.01f : lastSliderValue;
    }

    public void setSpeed()
    {
        this.speed = speedController.value;
        speedText.text = this.speed.ToString();
    }
}
