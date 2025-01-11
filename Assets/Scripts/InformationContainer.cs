using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationContainer<T> : MonoBehaviour , IInformationContainer
{
    // Start is called before the first frame update
    public T Content { get;set; }
    public string ContentTag { get;set; }   
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        

    }

}
