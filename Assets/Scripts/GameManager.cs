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
    [SerializeField] Color playerTeamColor;
    [SerializeField] Color enemyTeamColor;
    [SerializeField] Timer timer;
    [SerializeField] Transform playerSpawn;
    [SerializeField] Transform enemySpawn;
    [SerializeField] GameObject team;
    //SaveDataHandler saveDataHandler;


    GameObject chosenWeapon;
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
        // Done like this so we can add more containers without modyfying the class (open-close)
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
        
        // Timer created
        
        timer.TimeLeft = gameLength;
        timer.OnGameEnd += OnGameEnd;

        // create the teams
        playerTeam = Instantiate(team).GetComponent<Team>();
        playerTeam.SetTeamColor(playerTeamColor);
        playerTeam.SetTeamWeapon(chosenWeapon);
        playerTeam.createTeamMembers(true, playerSpawn.position, cinemachineCamera, paths);

        enemyTeam = Instantiate(team).GetComponent<Team>();
        enemyTeam.SetTeamColor(enemyTeamColor);
        enemyTeam.SetTeamWeapon(chosenWeapon);
        enemyTeam.createTeamMembers(false, enemySpawn.position, cinemachineCamera, paths);



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
        SceneManager.LoadScene("Menu");
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
