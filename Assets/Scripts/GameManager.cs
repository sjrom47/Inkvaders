using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    // Start is called before the first frame update
    SaveDataHandler saveDataHandler;
    void Start()
    {
        // Don't have time for this
        saveDataHandler = SaveDataHandler.Instance();
        saveDataHandler.LoadSaveData();
        GameManager.EnableScenePersistance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
