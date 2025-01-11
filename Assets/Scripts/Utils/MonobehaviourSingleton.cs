using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSingleton<T> :  MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T Instance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<T>();
            if (instance == null)
            {
                var singletonObj = new GameObject();
                singletonObj.name = typeof(T).ToString();
                instance = singletonObj.AddComponent<T>();
            }
        }
        return instance;
    }
    
}
