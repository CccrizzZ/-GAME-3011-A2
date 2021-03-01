using System;
using UnityEngine;

public class LockPickingCanvasScript : MonoBehaviour
{

    public GameObject ToolRef;
    public GameObject TargetLineRef;
    public GameObject[] PinsRefArray;
    public GameObject[] SpringRefArray;
    public GameObject startLine;
    public GameObject UnlockButton;



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

    int TotalPinSlot;

    
    

    void Start()
    {

        TotalPinSlot = PinsRefArray.Length + 1;


        ToolPosition = 1;
        Unlocked = false;




        // randomize unlock level for each pin
        foreach (var pin in PinsRefArray)
        {
            print("Adj Level");
            pin.transform.GetChild(0).transform.position = new Vector3(
                pin.transform.GetChild(0).transform.position.x,
                pin.transform.GetChild(0).transform.position.y + UnityEngine.Random.Range(5f,-5f),
                pin.transform.GetChild(0).transform.position.z
            );
            print(pin.transform.GetChild(0).transform.position.y);
        }
        



    }


    void Update()
    {
        // get pin level
        PinsInitLevel = startLine.transform.position.y;
        

        // 
        // If key is on the 5th pin lock is picked
        if (ToolPosition == TotalPinSlot)
        {
            Unlocked = true;
            UnlockButton.SetActive(true);
        }
        else
        {
            Unlocked = false;
            UnlockButton.SetActive(false);

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
            if (ToolPosition < TotalPinSlot)
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
            if (ToolPosition < TotalPinSlot)
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
            // index of pin
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
    }









    // shift the tool back and forth
    void ShiftPickingTool(int diff)
    {
        // go next
        if (ToolPosition + 1 <= TotalPinSlot && diff == 1)
        {
            // set tool position
            ToolPosition += diff;

            // set tool transform
            ToolRef.transform.position += transform.right * 0.8f;



        }
        

        // go previous
        if(ToolPosition - 1 >= 1 && diff == -1)
        {
            // set tool position
            ToolPosition += diff;
        
            // set tool transform
            ToolRef.transform.position -= transform.right * 0.8f;
        }
    }
}
