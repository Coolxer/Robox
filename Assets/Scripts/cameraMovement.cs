using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject workspace;
    public GameObject target;
    public float zoomspeed = 1000.0f;
    public float rotatespeed = 10.0f;

    private DraggingManager draggingManager;

    void Start()
    {
        draggingManager = workspace.GetComponent<DraggingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
        zoom(); 
    }

    private void rotate()
    {
        if(!GetComponent<Camera>().enabled) return;
            
        if (draggingManager.isDragging())
            transform.RotateAround(target.transform.position, target.transform.up, Input.GetAxis("Mouse X") * rotatespeed); 
    }

    private void zoom()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        // zoom in
        if (Input.mouseScrollDelta.y > 0 && distance >= 500.0f)
            transform.position += transform.forward * zoomspeed * Time.deltaTime;
 
        // zoom out
        if (Input.mouseScrollDelta.y < 0 && distance <= 1800.0f)
            transform.position -= transform.forward * zoomspeed * Time.deltaTime;
    }
}
