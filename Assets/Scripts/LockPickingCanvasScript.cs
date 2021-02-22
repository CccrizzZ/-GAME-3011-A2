using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickingCanvasScript : MonoBehaviour
{

    public GameObject ToolRef;
    public GameObject TargetLineRef;
    public GameObject[] PinsRefArray;
    public GameObject[] SpringRefArray;



    int ToolPosition;
    bool Unlocked;
    
    bool up;

    float PinsInitLevel;



    void Start()
    {
        ToolPosition = 1;
        Unlocked = false;


        PinsInitLevel = PinsRefArray[0].transform.position.y;
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

        if (Input.GetKeyDown("w"))
        {
            print("111");
            up = true;

        }


        if (Input.GetKeyUp("w"))
        {
            print("222");
            up = false;
        
        }

        // w key pressed
        if (up)
        {
            if(PinsRefArray[ToolPosition-1].transform.position.y <= PinsInitLevel + 1f)
            {
                PinsRefArray[ToolPosition-1].transform.position = new Vector3(
                    PinsRefArray[ToolPosition-1].transform.position.x,
                    PinsRefArray[ToolPosition-1].transform.position.y + 0.005f,
                    PinsRefArray[ToolPosition-1].transform.position.z
                );
            }

            if (SpringRefArray[ToolPosition-1].transform.localScale.y > 0.25f)
            {
                SpringRefArray[ToolPosition-1].transform.localScale = new Vector3(
                    SpringRefArray[ToolPosition-1].transform.localScale.x,
                    SpringRefArray[ToolPosition-1].transform.localScale.y - 0.0048f,
                    SpringRefArray[ToolPosition-1].transform.localScale.z
                );
            }
        }



        // w key released
        if(!up)
        {
            if(PinsRefArray[ToolPosition-1].transform.position.y >= PinsInitLevel + 0.18f)
            {
                PinsRefArray[ToolPosition-1].transform.position = new Vector3(
                    PinsRefArray[ToolPosition-1].transform.position.x,
                    PinsRefArray[ToolPosition-1].transform.position.y - 0.005f,
                    PinsRefArray[ToolPosition-1].transform.position.z
                );

            }


            if (SpringRefArray[ToolPosition-1].transform.localScale.y < 1f)
            {
                SpringRefArray[ToolPosition-1].transform.localScale = new Vector3(
                    SpringRefArray[ToolPosition-1].transform.localScale.x,
                    SpringRefArray[ToolPosition-1].transform.localScale.y + 0.0048f,
                    SpringRefArray[ToolPosition-1].transform.localScale.z
                );
            }
        }


    }



    // shift the tool back and forth
    void ShiftPickingTool(int diff)
    {
        // go next
        if (ToolPosition + 1 <= 4 && diff == 1)
        {
            // set tool position
            ToolPosition += diff;

            // set tool transform
            ToolRef.transform.position = new Vector3(
                ToolRef.transform.position.x + 0.8f,
                ToolRef.transform.position.y,
                ToolRef.transform.position.z
            );
        }
        

        // go previous
        if(ToolPosition - 1 >= 1 && diff == -1)
        {
            // set tool position
            ToolPosition += diff;

            // set tool transform
            ToolRef.transform.position = new Vector3(
                ToolRef.transform.position.x - 0.8f,
                ToolRef.transform.position.y,
                ToolRef.transform.position.z
            );
        }

        // print(ToolPosition);

    }


}
