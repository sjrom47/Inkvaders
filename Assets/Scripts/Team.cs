using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Team : MonoBehaviour
{
    // Start is called before the first frame update
    List<Player> teamMembers;
    PlayerBuilder builder;
    GameObject weaponPrefab;
    Color color;
    int members = 3;
    public int Members { get { return members; } set { members = value; } }

    void Start()
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

    void createTeamMembers(bool hasPlayablePlayer, Vector3 spawnPosition, GameObject cinemachineCamera) 
    {
        if (hasPlayablePlayer) 
        {
            CreatePlayableCharacter(spawnPosition, cinemachineCamera);
        }
        for (int i = 0; i < (hasPlayablePlayer ? members - 1 : members); i++)
        {
            //TODO: modify spawn position slightly for AI based on index
            CreateAIPlayer();
        }
    }

    void CreatePlayableCharacter(Vector3 spawnPosition, GameObject cinemachineCamera)
    {
        // TODO: check if the input manager can be searched with GetComponent
        builder.StartCreatingPlayer(Vector3.zero);
        builder.AddCamera2Player(cinemachineCamera);
        builder.AddInputManager(GetComponent<InputManager>());
        builder.AssignColor2Player(color);
        builder.AssignWeapon2Player(weaponPrefab);
        teamMembers.Add(builder.BuildPlayer());
    }

    void CreateAIPlayer()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
