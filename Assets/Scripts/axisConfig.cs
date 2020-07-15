using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisConfig : MonoBehaviour
{
    public ValueController position;

    public ValueController speed;

    public Button autoButton;

    public Text autoText;

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
            text = "AUTO";
            color = Color.green;

            autoText.color = new Color(0.25f, 0.25f, 0.25f);
            autoText.text = "---";
        }
        else
        {
            text = "STOP";
            color = Color.red;

            autoText.color = direction == -1 ? Color.red : Color.green;
            autoText.text = direction == -1 ? "LEFT" : "RIGHT";
        }

        autoButton.GetComponentInChildren<Text>().text = text;
        autoButton.GetComponent<Image>().color = color;
    }

    private void autoRotate()
    {
         if(position.getValue() >= position.getMax() || position.getValue() <= position.getMin()) 
            changeDirection();

        float newValue = position.getLastValue() + (speed.getValue() * Time.deltaTime * direction); // 5 is speed
        float delta = newValue - position.getLastValue();
        transform.Rotate(vector * delta);

        position.setValue(newValue);
    }

    private void changeDirection()
    {
        direction *= -1;

        if(direction == -1)
        {
            autoText.text = "LEFT";
            autoText.color = Color.red;
            
        }
        else if (direction == 1)
        {
            autoText.text = "RIGHT";
            autoText.color = Color.green;
        }
    }
}
