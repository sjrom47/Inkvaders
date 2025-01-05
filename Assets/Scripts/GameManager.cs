using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    SaveDataHandler saveDataHandler;
    void Start()
    {
        saveDataHandler = SaveDataHandler.Instance();
        saveDataHandler.LoadSaveData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
