using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public Button camButton;

    private int currentCameraIndex = 0;
    private int lastCameraIndex = 0;
    private Camera[] cameras = new Camera[5];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            cameras[i] = transform.Find("cam" + i.ToString()).GetComponent<Camera>();
            cameras[i].enabled = false;
        }

        cameras[currentCameraIndex].enabled = true;

        camButton.onClick.AddListener(changeCamera);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeCamera()
    {
        lastCameraIndex = currentCameraIndex;

        if(currentCameraIndex == 4)
            currentCameraIndex = 0;
        else
            currentCameraIndex++;

        cameras[lastCameraIndex].enabled = false;
        cameras[currentCameraIndex].enabled = true;

        camButton.GetComponentInChildren<Text>().text = "CAM " + (currentCameraIndex + 1).ToString();
    }
}
