using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    // Start is called before the first frame update
    [SerializeField] int membersPerTeam;
    SaveDataHandler saveDataHandler;
    GameObject chosenWeapon;
    void Awake()
    {
        // Don't have time for this
        saveDataHandler = SaveDataHandler.Instance();
        saveDataHandler.LoadSaveData();

        GameManager.EnableScenePersistance();
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        // TODO: see how to start the team creation, timer... (event?)
        if (scene.name != "Menu")
        {

        }
    }

    void OnGameEnd()
    {
        Dictionary<Color,int> colorCounts = PaintManager.Instance().OnGameEnd();
        // TODO: make the event for the module that finds the winner
    }

    public void ChangeChosenWeapon(GameObject chosenWeapon)
    {
        this.chosenWeapon = chosenWeapon;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
