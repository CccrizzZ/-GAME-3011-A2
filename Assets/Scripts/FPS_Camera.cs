using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPS_Camera : MonoBehaviour
{


    public float MouseSensitivity = 100.0f;
    public Transform PlayerBody;
    


    float XRotation = 0.0f;

    public bool canLook;



    void Start()
    {
        canLook = false;
    }


    public void SetCameraMove()
    {
        canLook = true;
        // lock cursor to the center of screen
        Cursor.lockState = CursorLockMode.Locked;
    
    
    }

    public void SetCameraFreeze()
    {
        canLook = false;
        Cursor.lockState = CursorLockMode.None;

    }


    void Update()
    {
        if (canLook)
        {
            // set mouse input
            float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
            

            // // clamp rotation to +90 and -90 deg
            XRotation -= MouseY;
            XRotation = Mathf.Clamp(XRotation, -90.0f, 90.0f);


            
            // rotate player body
            transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
            PlayerBody.Rotate(Vector3.up * MouseX);

        
            
        }
    }
}
