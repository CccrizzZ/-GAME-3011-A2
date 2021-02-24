using System;
using UnityEngine;

public class LockPickingCanvasScript : MonoBehaviour
{

    public GameObject ToolRef;
    public GameObject TargetLineRef;
    public GameObject[] PinsRefArray;
    public GameObject[] SpringRefArray;



    int ToolPosition;
    float PinsInitLevel;
    bool Unlocked;
    bool up;


    [SerializeField]
    float SpringSpeed;
    
    [SerializeField]
    float PinSpeed;
    [SerializeField]
    float PinLevelTolerance;    



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
                pin.transform.GetChild(0).transform.position.y + UnityEngine.Random.Range(0.24f,-0.24f),
                pin.transform.GetChild(0).transform.position.z
            );
        }
        



    }


    void Update()
    {

        // 
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
            // if pin level matchs unlocking level, goto the nect pin
            if (PinsRefArray[ToolPosition-1].transform.GetChild(0).transform.position.y <= TargetLineRef.transform.position.y + PinLevelTolerance 
            && PinsRefArray[ToolPosition-1].transform.GetChild(0).transform.position.y >= TargetLineRef.transform.position.y - PinLevelTolerance )
            {
                ShiftPickingTool(1);
            }
        }


        // pull the key outwards
        if(Input.GetKeyDown("a"))
        {
            ShiftPickingTool(-1);
        }


        // push the pin up
        if (Input.GetKeyDown("w"))
        {
            up = true;
        }

        // let the pin down
        if (Input.GetKeyUp("w"))
        {
            up = false;
        }        


            
        // w key pressed
        if (up && !Unlocked)
        {
            if (ToolPosition < 5)
            {
                // set pin transform
                if(PinsRefArray[ToolPosition-1].transform.position.y <= PinsInitLevel + 1f)
                {
                    PinsRefArray[ToolPosition-1].transform.position = new Vector3(
                        PinsRefArray[ToolPosition-1].transform.position.x,
                        PinsRefArray[ToolPosition-1].transform.position.y + PinSpeed,
                        PinsRefArray[ToolPosition-1].transform.position.z
                    );
                }

                // set spring scale
                if (SpringRefArray[ToolPosition-1].transform.localScale.y > 0.25f)
                {
                    SpringRefArray[ToolPosition-1].transform.localScale = new Vector3(
                        SpringRefArray[ToolPosition-1].transform.localScale.x,
                        SpringRefArray[ToolPosition-1].transform.localScale.y - SpringSpeed,
                        SpringRefArray[ToolPosition-1].transform.localScale.z
                    );
                }
            }
        }



        // w key released
        if(!up)
        {
            if (ToolPosition < 5)
            {
                // set pin transform
                if(PinsRefArray[ToolPosition-1].transform.position.y >= PinsInitLevel + 0.18f)
                {
                    PinsRefArray[ToolPosition-1].transform.position = new Vector3(
                        PinsRefArray[ToolPosition-1].transform.position.x,
                        PinsRefArray[ToolPosition-1].transform.position.y - PinSpeed,
                        PinsRefArray[ToolPosition-1].transform.position.z
                    );

                }

                // set spring scale
                if (SpringRefArray[ToolPosition-1].transform.localScale.y < 1f)
                {
                    SpringRefArray[ToolPosition-1].transform.localScale = new Vector3(
                        SpringRefArray[ToolPosition-1].transform.localScale.x,
                        SpringRefArray[ToolPosition-1].transform.localScale.y + SpringSpeed,
                        SpringRefArray[ToolPosition-1].transform.localScale.z
                    );
                }
            }
        }



        // let the pin and spring come down if tool pulled back
        foreach (var pin in PinsRefArray)
        {
            var Pindex = Array.IndexOf(PinsRefArray, pin);
            if (Pindex > ToolPosition - 1 && pin.transform.position.y >= PinsInitLevel + 0.18f)
            {
                pin.transform.position = new Vector3(
                    pin.transform.position.x,
                    pin.transform.position.y - PinSpeed,
                    pin.transform.position.z
                );
            }
        }
        
        foreach (var spring in SpringRefArray)
        {
            var Sindex = Array.IndexOf(SpringRefArray, spring);
            if (Sindex > ToolPosition - 1 && spring.transform.localScale.y < 1f)
            {
                spring.transform.localScale = new Vector3(
                    spring.transform.localScale.x,
                    spring.transform.localScale.y + SpringSpeed,
                    spring.transform.localScale.z
                );
            }
        }

        print(up);

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


    }


}
