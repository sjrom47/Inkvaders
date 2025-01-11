using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InformationContainer : MonoBehaviour 
{
    // Start is called before the first frame update
    
    public string ContentTag { get;set; }   
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        

    }

}
