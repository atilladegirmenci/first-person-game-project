using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour
{
   
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
    }

    // public RaycastHit Point()
    //{
    //    RaycastHit hit;
    //    Vector3 look = transform.TransformDirection(Vector3.forward);
    //    Physics.Raycast(transform.position, look, out hit);
    //    return hit;
    //}
}
