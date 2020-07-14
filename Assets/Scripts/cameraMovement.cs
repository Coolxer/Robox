using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public float zoomspeed = 1000.0f;
    public float rotatespeed = 100.0f;

    private Vector3 lastPos = Vector3.zero;
    private Vector3 lastDelta = Vector3.zero;

    void Start()
    {
        transform.LookAt(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
        zoom(); 
    }

    private void rotate()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastPos;

             if(Vector3.Dot(transform.up, Vector3.up) >= 0)
                target.transform.Rotate(target.transform.up, -Vector3.Dot(delta, transform.right), Space.World);
            else
                target.transform.Rotate(target.transform.up, Vector3.Dot(delta, transform.right), Space.World);
        
            target.transform.Rotate(transform.right, Vector3.Dot(delta, transform.up), Space.World);
        }

        lastPos = Input.mousePosition;
    }

    private void zoom()
    {
        if (Input.mouseScrollDelta.y > 0 && transform.position.z <= 500.0f)
            transform.position += transform.forward * zoomspeed * Time.deltaTime;
 
        if (Input.mouseScrollDelta.y < 0 && transform.position.z >= -1800.0f)
            transform.position -= transform.forward * zoomspeed * Time.deltaTime;
    }
}
