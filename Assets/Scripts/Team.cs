using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Team : MonoBehaviour
{
    // Start is called before the first frame update
    List<Player> teamMembers;
    PlayerBuilder builder;
    GameObject weaponPrefab;
    GameObject cinemachineCamera;
    List<Path> paths;
    InputManager inputManager;
    Color color;
    int members = 3;
    public int Members { get { return members; } set { members = value; } }

    void Awake()
    {
        teamMembers = new List<Player>();
        builder = GetComponent<PlayerBuilder>();
    }
    public void SetTeamWeapon(GameObject weaponPrefab)
    {
        this.weaponPrefab = weaponPrefab;
    }

    public void SetTeamColor(Color color) 
    {
        this.color = color;
    }

    public Color GetTeamColor()
    {
        return color;
    }

    public void SetCinemachine(GameObject cinemachineCamera)
    {
        this.cinemachineCamera = cinemachineCamera;
    }

    public void SetPaths(List<Path> paths)
    {
        this.paths = paths;
    }

    public void SetInputManager(InputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    public void createTeamMembers(bool hasPlayablePlayer, Vector3 spawnPosition) 
    {
        if (hasPlayablePlayer) 
        {
            
            CreatePlayableCharacter(spawnPosition);
        }
        for (int i = 0; i < (hasPlayablePlayer ? members - 1 : members); i++)
        {
            Path path = paths[i];
            Vector3 newSpawnPosition = new Vector3(spawnPosition.x + Mathf.Pow(-1,i)*2*((float)(int)(i/2)+1), spawnPosition.y, spawnPosition.z);

            CreateAIPlayer(newSpawnPosition, path);
        }
    }

    void CreatePlayableCharacter(Vector3 spawnPosition)
    {   
        builder.StartCreatingPlayer(spawnPosition);
        builder.AddCamera2Player(cinemachineCamera);
        builder.AddInputManager(inputManager);
        builder.AssignColor2Player(color);
        builder.AssignWeapon2Player(weaponPrefab);
        teamMembers.Add(builder.BuildPlayer());
    }

    void CreateAIPlayer(Vector3 spawnPosition, Path path)
    {
        builder.StartCreatingPlayer(spawnPosition);
        builder.AssignColor2Player(color);
        builder.AssignWeapon2Player(weaponPrefab);
        builder.AddEnemyController(path);
        teamMembers.Add(builder.BuildPlayer());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
