using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCreationTest : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerBuilder builder;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] InputManager inputManager;
    [SerializeField] Color color;
    [SerializeField] GameObject cinemachineCamera;
    [SerializeField] Path path;
    [SerializeField] int nBots;
    [SerializeField] int nPlayers;
    void Start()
    {
        builder = GetComponent<PlayerBuilder>();
        for (int i = 0; i < nBots; i++) 
        {
            BuildTestEnemy(new Vector3(48, 0, -21.5f));
        }
        for (int i = 0; i < nPlayers; i++)
        {
            BuildTestPlayer(new Vector3(47.5f, 0, -47.5f));
        }
    }

    void BuildTestPlayer(Vector3 position)
    {
        builder.StartCreatingPlayer(position);
        builder.AddCamera2Player(cinemachineCamera);
        builder.AddInputManager(inputManager);
        builder.AssignColor2Player(color);
        builder.AssignWeapon2Player(weaponPrefab);
        builder.BuildPlayer();
    }

    void BuildTestEnemy(Vector3 position)
    {
        builder.StartCreatingPlayer(position);
        builder.AssignColor2Player(color);
        builder.AssignWeapon2Player(weaponPrefab);
        builder.AddEnemyController(path);
        builder.BuildPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
