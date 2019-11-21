using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //onStart Delegates
    public delegate void onStart();
    public static event onStart StartOccurred;

    //onUpdate Delegates
    public delegate void onUpdate();
    public static event onUpdate UpdateOccurred;

    //Variables
     /*Public variables*/ 
    
    /*Private variables*/


    

    void Start()
    {
        if (StartOccurred != null)
        {
            StartOccurred();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateOccurred != null)
            UpdateOccurred();
    }
    
   
    
}

