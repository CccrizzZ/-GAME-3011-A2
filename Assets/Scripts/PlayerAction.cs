using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public bool ispicking = false;


    public Canvas LP_canvas_easy;
    public Canvas LP_canvas_medium;
    public Canvas LP_canvas_hard;
    public Camera camref;
    
    public GameObject targetlockpole;


    Canvas currentCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetlockpole != null)
        {
            if(Vector3.Distance(transform.position, targetlockpole.transform.position) > 4)
            {
                targetlockpole = null;
            }
            else
            {
                // print(targetlockpole.name);

                if (Input.GetKeyDown("e"))
                {
                    if (!ispicking)
                    {
                        ispicking = true;

                        gameObject.GetComponent<FPS_Movement>().SetCanMove(false);
                        gameObject.transform.Find("Main Camera").GetComponent<FPS_Camera>().SetCameraFreeze();

                        if(GameObject.FindGameObjectsWithTag("lockcanvas").GetLength(0)<=0)
                        {
                            
                            switch (targetlockpole.name)
                            {
                                case "Lock_easy":
                                    currentCanvas = Instantiate(LP_canvas_easy);
                                    break;
                                case "Lock_medium":
                                    currentCanvas = Instantiate(LP_canvas_medium);
                                    break;
                                case "Lock_hard":
                                    currentCanvas = Instantiate(LP_canvas_hard);
                                    break;
                                default:
                                    break;
                            }
                            
                            
                            
                            
                            
                            
                            currentCanvas.worldCamera = camref;
                            currentCanvas.planeDistance = 5;
                        }
                    }
                }
            }            
        }


    }
}
