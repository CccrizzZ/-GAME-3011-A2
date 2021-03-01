using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleScript : MonoBehaviour
{
    GameObject pref;
    
    public GameObject lightRef;


    private void Start() 
    {
        pref = GameObject.FindGameObjectWithTag("Player");
        
    }
   
    void Update()
    {
        // print(Vector3.Distance(pref.transform.position,transform.position));
        if(Vector3.Distance(pref.transform.position, transform.position) < 4 && lightRef != null)
        {
            lightRef.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>().targetlockpole = gameObject;        
        }
        else
        {
            lightRef.SetActive(false);
            // GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>().targetlockpole = null;        

        }

    }
}
