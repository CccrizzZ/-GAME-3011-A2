using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickingCanvasScript : MonoBehaviour
{

    public GameObject ToolRef;
    public GameObject TargetLineRef;


    int ToolPosition;
    bool Unlocked;





    void Start()
    {
        ToolPosition = 1;
        Unlocked = false;
    }


    void Update()
    {
        if(Input.GetKeyDown("d"))
        {
            ShiftPickingTool(1);
        }



        if(Input.GetKeyDown("a"))
        {
            ShiftPickingTool(-1);
        }
    }



    // shift the tool back and forth
    void ShiftPickingTool(int diff)
    {
        // set tool position
        ToolPosition += diff;

        // set tool transform
        ToolRef.transform.position = new Vector3(
            ToolRef.transform.position.x + 0.4f * diff,
            ToolRef.transform.position.y,
            ToolRef.transform.position.z
        );
    }


}
