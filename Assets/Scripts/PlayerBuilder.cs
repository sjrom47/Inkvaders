using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerBuilder : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab; // Prefab with all required components
    Player currentlyBuiltPlayer;

    public void StartCreatingPlayer(Vector3 position)
    {
        if (currentlyBuiltPlayer != null)
        {
            //Destroy(currentlyBuiltPlayer.gameObject);
            currentlyBuiltPlayer = null;
        }
        // Instantiate the prefab
        GameObject playerGO = Instantiate(playerPrefab, position, Quaternion.identity);

        // Get the Player component for configuration
        currentlyBuiltPlayer = playerGO.GetComponent<Player>();

        if (currentlyBuiltPlayer == null)
        {
            Debug.LogError("Player component is missing on the prefab!");
            return;
        }
        currentlyBuiltPlayer.SpawnPoint = position;

        // Ensure StateMachine exists
        PlayerStateMachine stateMachine = currentlyBuiltPlayer.GetComponent<PlayerStateMachine>();
        if (stateMachine == null)
        {
            stateMachine = currentlyBuiltPlayer.gameObject.AddComponent<PlayerStateMachine>();
        }

        // Optionally deactivate the GameObject during setup
        playerGO.SetActive(false);
    }

    public void AssignColor2Player(Color color)
    {
        if (currentlyBuiltPlayer == null) return;

        currentlyBuiltPlayer.PlayerColor = color;
        Debug.Log(currentlyBuiltPlayer.PlayerColor);
        Debug.Log(color);

        // Apply the color to other dependent components if needed
        WeaponHolder weaponHolder = currentlyBuiltPlayer.GetComponentInChildren<WeaponHolder>();
        if (weaponHolder != null)
        {
            weaponHolder.AssignColor2Weapon(color);
        }
    }

    public void AssignWeapon2Player(GameObject weaponPrefab)
    {
        if (currentlyBuiltPlayer == null) return;
        weaponPrefab.SetActive(true);

        WeaponHolder weaponHolder = currentlyBuiltPlayer.GetComponentInChildren<WeaponHolder>();
        if (weaponHolder == null)
        {
            Debug.LogError("WeaponHolder is missing!");
            return;
        }

        weaponHolder.EquipWeapon(weaponPrefab);
        Weapon weapon = weaponHolder.GetCurrentWeapon();
        Movementcopy copier = weapon.gameObject.GetComponent<Movementcopy>();
        if (copier != null)
        {
            copier.SetTarget(weaponHolder.GetWeaponPlacement());
        }

        // Ensure weapon inherits player's color
        if (currentlyBuiltPlayer.PlayerColor != null)
        {
            weaponHolder.AssignColor2Weapon(currentlyBuiltPlayer.PlayerColor);
        }
    }

    public void AddInputManager(InputManager inputManager)
    {
        if (currentlyBuiltPlayer == null) return;

        // Ensure PlayerController exists
        PlayerController playerController = currentlyBuiltPlayer.GetComponent<PlayerController>();
        if (playerController == null)
        {
            playerController = currentlyBuiltPlayer.gameObject.AddComponent<PlayerController>();
        }

        // Add InputManager and pass PlayerController reference
        // Attach the provided InputManager instance to the player
        inputManager = currentlyBuiltPlayer.gameObject.AddComponent(inputManager.GetType()) as InputManager;
        inputManager.SetPlayerController(playerController);

        currentlyBuiltPlayer.tag = "Player";
    }

    

    public void AddCamera2Player(GameObject camera)
    {
        Transform playerTransform = currentlyBuiltPlayer.gameObject.transform;
        if (currentlyBuiltPlayer == null) return;
        CinemachineFreeLook cinemachineCamera = camera.GetComponent<CinemachineFreeLook>();
        if (cinemachineCamera != null)
        {
            cinemachineCamera.LookAt = playerTransform;
            cinemachineCamera.Follow = playerTransform;
        }
        CameraRotationController cameraRotationController = camera.GetComponent<CameraRotationController>();
        if (cameraRotationController != null)
        {
            cameraRotationController.SetPlayerTransform(playerTransform);
        }
    }

    public void AddEnemyController(Path path)
    {
        if (currentlyBuiltPlayer == null) return;

        // Ensure StateMachine exists
        EnemyStateMachine stateMachine = currentlyBuiltPlayer.GetComponent<EnemyStateMachine>();
        if (stateMachine == null)
        {
            stateMachine = currentlyBuiltPlayer.gameObject.AddComponent<EnemyStateMachine>();
        }

        // Ensure NavMeshAgent exists
        NavMeshAgent navMeshAgent = currentlyBuiltPlayer.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            navMeshAgent = currentlyBuiltPlayer.gameObject.AddComponent<NavMeshAgent>();
        }

        navMeshAgent.radius = 0.4f;
        navMeshAgent.height = 2;
        navMeshAgent.speed = 8;
        navMeshAgent.acceleration = 5;
        navMeshAgent.angularSpeed = 170;
        navMeshAgent.autoBraking = true;

        // Ensure PlayerController exists
        PlayerController playerController = currentlyBuiltPlayer.GetComponent<PlayerController>();
        if (playerController == null)
        {
            playerController = currentlyBuiltPlayer.gameObject.AddComponent<PlayerController>();
        }

        // Ensure Enemy exists
        Enemy enemy = currentlyBuiltPlayer.GetComponent<Enemy>();
        if (enemy == null)
        {
            enemy = currentlyBuiltPlayer.gameObject.AddComponent<Enemy>();
        }

        enemy.path = path;
    }

    public Player BuildPlayer()
    {
        if (currentlyBuiltPlayer != null)
        {
            // Activate the GameObject when fully built
            Debug.Log('a');
            currentlyBuiltPlayer.gameObject.SetActive(true);
        }
        return currentlyBuiltPlayer;
    }
}

