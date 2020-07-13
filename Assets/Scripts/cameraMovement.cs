using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public float zoomspeed = 1000.0f;
    public float rotatespeed = 100.0f;

    void Start()
    {
        transform.LookAt(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.down, Input.GetAxis("Horizontal") * rotatespeed * Time.deltaTime);
 
        if (Input.mouseScrollDelta.y > 0)
            transform.position += transform.forward * zoomspeed * Time.deltaTime;
 
        if (Input.mouseScrollDelta.y < 0)
            transform.position -= transform.forward * zoomspeed * Time.deltaTime;
    }
}
