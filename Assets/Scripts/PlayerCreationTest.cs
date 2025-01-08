using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreationTest : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerBuilder builder;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] InputManager inputManager;
    [SerializeField] Color color;
    [SerializeField] GameObject cinemachineCamera;
    void Start()
    {
        builder = GetComponent<PlayerBuilder>();
        BuildTestPlayer();
    }

    void BuildTestPlayer()
    {
        builder.StartCreatingPlayer();
        builder.AddCamera2Player(cinemachineCamera);
        builder.AddInputManager(inputManager);
        builder.AssignColor2Player(color);
        builder.AssignWeapon2Player(weaponPrefab);
        builder.BuildPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}