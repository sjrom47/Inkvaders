using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    // Start is called before the first frame update
    [SerializeField] int membersPerTeam;
    [SerializeField] List<Path> paths;
    [SerializeField] GameObject cinemachineCamera;
    [SerializeField] float gameLength;
    //SaveDataHandler saveDataHandler;
    GameObject chosenWeapon;
    Timer timer;
    Team playerTeam;
    Team enemyTeam;
    Dictionary<string, IContainerLoader> containerLoaders;
    public event Action OnGameStart;
    public event Action<string> ShowWinner;
    void Awake()
    {
        // Don't have time for this
        //saveDataHandler = SaveDataHandler.Instance();
        //saveDataHandler.LoadSaveData();
        // done like this so we can add more containers without modyfying the class (open-close)
        containerLoaders = new Dictionary<string, IContainerLoader>() { {"Weapon", new WeaponLoader() } };

        List<IInformationContainer> infoContainers = FindObjectsOfType<MonoBehaviour>().OfType<IInformationContainer>().ToList();
        foreach (IInformationContainer container in infoContainers)
        {
            if (container is InformationContainer<object> infoContainer)
            {
                string tag = infoContainer.ContentTag;
                if (containerLoaders.ContainsKey(tag))
                {
                    containerLoaders[tag].LoadInformationContainer(container);
                }
            }
        }
        // TODO: create the teams
        timer = GetComponent<Timer>();
        timer.TimeLeft = gameLength;
        timer.OnGameEnd += OnGameEnd;
        
        
    }
    private void Start()
    {
        OnGameStart?.Invoke();
    }

    private void OnDestroy()
    {
        timer.OnGameEnd -= OnGameEnd;
    }

    void OnGameEnd()
    {
        Dictionary<Color,int> colorCounts = PaintManager.Instance().OnGameEnd();
        // It returns the team so we could move or show the players somehow
        Team winningTeam = WinnerCalculator.FindWinningTeam(playerTeam, enemyTeam, colorCounts);
        string message = winningTeam == playerTeam ? "You won this battle" : "You lost this battle";
        // show the text
        ShowWinner?.Invoke(message);
    }

    public void SetChosenWeapon(GameObject chosenWeapon)
    {
        this.chosenWeapon = chosenWeapon;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
