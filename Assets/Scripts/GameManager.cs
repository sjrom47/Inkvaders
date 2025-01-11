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
    [SerializeField] InputManager inputManager;
    [SerializeField] GameObject chosenWeapon;
    //SaveDataHandler saveDataHandler;



    Team playerTeam;
    Team enemyTeam;
    Dictionary<string, IContainerLoader> containerLoaders;
    List<InformationContainer> infoContainers;
    public event Action OnGameStart;
    public event Action<string> ShowWinner;
    void Awake()
    {
        // Don't have time for this
        //saveDataHandler = SaveDataHandler.Instance();
        //saveDataHandler.LoadSaveData();
        // Done like this so we can add more containers without modyfying the class (open-close)
        containerLoaders = new Dictionary<string, IContainerLoader>() { {"Weapons", new WeaponLoader() } };

        infoContainers = FindObjectsOfType<MonoBehaviour>().OfType<InformationContainer>().ToList();
        foreach (InformationContainer container in infoContainers)
        {
            
            if (container is InformationContainer infoContainer)
            {
                string tag = infoContainer.ContentTag;
                Debug.Log(tag);
                if (tag!=null && containerLoaders.ContainsKey(tag))
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
        playerTeam.Members = membersPerTeam;
        Debug.Log(chosenWeapon);
        playerTeam.SetTeamWeapon(chosenWeapon);
        playerTeam.SetCinemachine(cinemachineCamera);
        playerTeam.SetPaths(paths);
        playerTeam.SetInputManager(inputManager);
        playerTeam.createTeamMembers(true, playerSpawn.position);

        enemyTeam = Instantiate(team).GetComponent<Team>();
        enemyTeam.SetTeamColor(enemyTeamColor);
        enemyTeam.Members = membersPerTeam;
        enemyTeam.SetTeamWeapon(chosenWeapon);
        enemyTeam.SetCinemachine(cinemachineCamera);
        enemyTeam.SetPaths(paths);
        enemyTeam.createTeamMembers(false, enemySpawn.position);



    }
    private void Start()
    {
        OnGameStart?.Invoke();
    }



    void OnGameEnd()
    {
        Dictionary<Color,int> colorCounts = PaintManager.Instance().OnGameEnd();
        // It returns the team so we could move or show the players somehow
        Team winningTeam = WinnerCalculator.FindWinningTeam(playerTeam, enemyTeam, colorCounts);
        string message = winningTeam == playerTeam ? "You won this battle" : "You lost this battle";
        // show the text
        ShowWinner?.Invoke(message);
        timer.OnGameEnd -= OnGameEnd;
        foreach (InformationContainer container in infoContainers)
        {
            Destroy(container.gameObject);
        }
        StartCoroutine(LoadMenuCoroutine());
        
        //SceneManager.LoadScene("Menu");
    }
    IEnumerator LoadMenuCoroutine()
    {
        yield return new WaitForSeconds(5f);
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
