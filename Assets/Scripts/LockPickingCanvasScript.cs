using System.Linq;
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


        // get pin level
        PinsInitLevel = PinsRefArray[0].transform.position.y;


        // randomize unlock level for each pin
        foreach (var pin in PinsRefArray)
        {
            pin.transform.GetChild(0).transform.position = new Vector3(
                pin.transform.GetChild(0).transform.position.x,
                pin.transform.GetChild(0).transform.position.y + Random.Range(0.24f,-0.24f),
                pin.transform.GetChild(0).transform.position.z
            );
        }
        



    }


    void Update()
    {
        // If key is on the 5th pin lock is picked
        if (ToolPosition == 5)
        {
            Unlocked = true;
        }else
        {
            Unlocked = false;
        }


        // push the key inwards
        if(Input.GetKeyDown("d") && !Unlocked)
        {
            if (PinsRefArray[ToolPosition-1].transform.GetChild(0).transform.position.y <= TargetLineRef.transform.position.y + 0.01f 
            && PinsRefArray[ToolPosition-1].transform.GetChild(0).transform.position.y >= TargetLineRef.transform.position.y - 0.01f )
            {
                ShiftPickingTool(1);
                
            }
        }


        // pull the key outwards
        if(Input.GetKeyDown("a"))
        {
            ShiftPickingTool(-1);
        }

        if (!Unlocked)
        { 
            // push the pin up
            if (Input.GetKeyDown("w"))
            {
                print("111");
                up = true;
            }


            // let the pin down
            if (Input.GetKeyUp("w"))
            {
                print("222");
                up = false;
            }
                
        }



        // w key pressed
        if (up)
        {
            // set pin transform
            if(PinsRefArray[ToolPosition-1].transform.position.y <= PinsInitLevel + 1f)
            {
                PinsRefArray[ToolPosition-1].transform.position = new Vector3(
                    PinsRefArray[ToolPosition-1].transform.position.x,
                    PinsRefArray[ToolPosition-1].transform.position.y + 0.005f,
                    PinsRefArray[ToolPosition-1].transform.position.z
                );
            }

            // set spring scale
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
        if(!up && ToolPosition <= 4)
        {
            // set pin transform
            if(PinsRefArray[ToolPosition-1].transform.position.y >= PinsInitLevel + 0.18f)
            {
                PinsRefArray[ToolPosition-1].transform.position = new Vector3(
                    PinsRefArray[ToolPosition-1].transform.position.x,
                    PinsRefArray[ToolPosition-1].transform.position.y - 0.005f,
                    PinsRefArray[ToolPosition-1].transform.position.z
                );

            }

            // set spring scale
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
        if (ToolPosition + 1 <= 5 && diff == 1)
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
