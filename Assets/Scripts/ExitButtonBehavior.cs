using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonBehavior : MonoBehaviour
{

    public Canvas CanvasRef;
    
    public void OnExitButtonPressed()
    {
        Destroy(CanvasRef);
    }

}
