using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public Transform center;

    public Vector3 vector;

    public float speed = 20.0f;

    private float angleH = 0;
    private float angleV = 0;

    public GameObject blueAxis;

    public GameObject greenAxis;

    private Vector3 vb;
    private Vector3 vg;

    // Start is called before the first frame update
    void Start()
    {
        vb = Vector3.up;
        vg = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        // jesli obiekt nie jest aktywny nie bedzie podjeta zadna akcja
        // obiekt jest aktywny tylko w trybie "Inverse Kinematics"
        if(!gameObject.activeInHierarchy) return;

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("pressed");
            transform.LookAt(center, vb);
            if(angleH <= 165.0f)
            {
                float value = speed * Time.deltaTime;
                transform.RotateAround(center.position, vb, value);
                //blueAxis.transform.RotateAround(center.position, vb, value);
                //vg = Quaternion.AngleAxis(value, vb).eulerAngles;
                angleH += value;
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.LookAt(center, vb);
            if(angleH >= -165.0f)
            {
                float value = speed * Time.deltaTime;
                transform.RotateAround(center.position, -vb, value);
                //blueAxis.transform.RotateAround(center.position, -vb, value);
                //vg = Quaternion.AngleAxis(-value, vb).eulerAngles;
                angleH -= value;
            }
        }

        else if (Input.GetKey(KeyCode.W))
        {
            transform.LookAt(center, vg);
            if(angleV <= 55.0f)
            {
                float value = speed * Time.deltaTime;
                transform.RotateAround(center.position, vg, value);
                //greenAxis.transform.RotateAround(center.position, vg, value);
                //vb = Quaternion.AngleAxis(value, vg).eulerAngles;
                angleV += value;
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.LookAt(center, vg);
            if(angleV >= -80.0f)
            {
                float value = speed * Time.deltaTime;
                transform.RotateAround(center.position, -vg, value);
                //greenAxis.transform.RotateAround(center.position, -vg, value);
                //vb = Quaternion.AngleAxis(-value, vg).eulerAngles;
                angleV -= value;
            }
        }
    }
}
