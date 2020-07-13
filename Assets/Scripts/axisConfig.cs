using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisConfig : MonoBehaviour
{
    public ValueController position;

    public ValueController speed;

    public Button autoButton;

    public Vector3 vector;

    private bool auto = false;

    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {       
        autoButton.onClick.AddListener(setAutoRotate);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(auto) autoRotate();
        else if (position.getDelta() != 0)
        {
            transform.Rotate(vector * position.getDelta());
            position.resetDelta();
        }
    }

    public void setAutoRotate()
    {
        auto = !auto;

        string text;
        Color color;

        if(!auto)
        {
            text = "Auto";
            color = Color.green;
        }
        else
        {
            text = "Stop";
            color = Color.red;
        }

        autoButton.GetComponentInChildren<Text>().text = text;
        autoButton.GetComponent<Image>().color = color;
        
    }

    private void autoRotate()
    {
        float newValue = position.getLastValue() + (speed.getValue() * Time.deltaTime * direction); // 5 is speed
        float delta = newValue - position.getLastValue();
        transform.Rotate(vector * delta);

        position.setValue(newValue);

        if(newValue >= position.getMax() && direction == 1) direction = -1;
        else if(newValue <= position.getMin() && direction == -1) direction = 1;
    }
}
