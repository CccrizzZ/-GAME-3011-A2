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
        // canLook = false;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        SetCameraMove();
    }



    // Game mode
    public void SetCameraMove()
    {
        // lock cursor to the center of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canLook = true;
    
    
    }

    // UI mode
    public void SetCameraFreeze()
    {
        // Dont lock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canLook = false;

    }


    void Update()
    {

        // Debug.DrawRay(transform.position - new Vector3(0,0,1), transform.TransformDirection(Vector3.forward) * 1000, Color.red);
        if (canLook)
        {
            // set mouse input
            float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
            

            // // clamp rotation to +90 and -90 deg
            XRotation -= MouseY;
            XRotation = Mathf.Clamp(XRotation, -25.0f, 25.0f);


            
            // rotate player body
            transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
            PlayerBody.Rotate(Vector3.up * MouseX);

        
            
        }
    }
}
