using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonBehavior : MonoBehaviour
{

    public Canvas CanvasRef;
    
    GameObject pref; 


    void Start()
    {
        pref = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnExitButtonPressed()
    {
        Destroy(CanvasRef.gameObject);
        pref.GetComponent<FPS_Movement>().SetCanMove(true);
        pref.transform.Find("Main Camera").GetComponent<FPS_Camera>().SetCameraMove();
        pref.GetComponent<PlayerAction>().ispicking = false;



    }

}
