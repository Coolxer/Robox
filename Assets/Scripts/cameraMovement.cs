using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public GameObject target;

    private Vector3 lastPosition;

    public float speed = 100.0f;

    private float angleH = 0, angleV = 0;

    private float distance = 10.0f;

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        distance += Input.GetAxis("Mouse ScrollWheel");

        transform.RotateAround(target.transform.position, Vector3.up, -Input.GetAxis("Horizontal") * speed * Time.deltaTime);

/*
        if(moveHorizontal != 0) 
            angleH += moveHorizontal;

        if(moveVertical != 0)
            angleV += moveVertical;

        Vector3 tmp;
        tmp.x = Mathf.Cos(angleH * (Mathf.PI / 180)) * Mathf.Sin(angleV * (Mathf.PI / 180)) * distance + target.transform.position.x;
        tmp.z = Mathf.Sin(angleH * (Mathf.PI / 180)) * Mathf.Sin(angleV * (Mathf.PI / 180)) * distance + target.transform.position.z;
        tmp.y = Mathf.Sin(angleV * (Mathf.PI / 180)) * distance + target.transform.position.y;
        transform.position = Vector3.Slerp(transform.position, tmp, speed * Time.deltaTime);
        transform.LookAt(target.transform);

        */

        /*
        if(Input.GE)       
            lastPosition = camera.ScreenToViewportPoint(Input.mousePosition);
   
        else if(Input.GetMouseButton(0))
        {
            Vector3 direction = lastPosition - camera.ScreenToViewportPoint(Input.mousePosition);

            camera.transform.position = new Vector3();

            camera.transform.Rotate(Vector3.right, direction.y * 180);
            camera.transform.Rotate(Vector3.down, direction.y * 180, Space.World);

            camera.transform.Translate(new Vector3(0 ,0, -10));

            lastPosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }
        */
    }
}
