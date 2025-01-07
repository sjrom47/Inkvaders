using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBuilder : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab; // Prefab with all required components
    Player currentlyBuiltPlayer;

    public void StartCreatingPlayer()
    {
        if (currentlyBuiltPlayer != null)
        {
            Destroy(currentlyBuiltPlayer.gameObject);
        }
        // Instantiate the prefab
        GameObject playerGO = Instantiate(playerPrefab);

        // Get the Player component for configuration
        currentlyBuiltPlayer = playerGO.GetComponent<Player>();

        if (currentlyBuiltPlayer == null)
        {
            Debug.LogError("Player component is missing on the prefab!");
            return;
        }

        // Optionally deactivate the GameObject during setup
        playerGO.SetActive(false);
    }

    public void AssignColor2Player(Color color)
    {
        if (currentlyBuiltPlayer == null) return;

        currentlyBuiltPlayer.PlayerColor = color;

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

        WeaponHolder weaponHolder = currentlyBuiltPlayer.GetComponentInChildren<WeaponHolder>();
        if (weaponHolder == null)
        {
            Debug.LogError("WeaponHolder is missing!");
            return;
        }

        weaponHolder.EquipWeapon(weaponPrefab);

        // Ensure weapon inherits player's color
        if (currentlyBuiltPlayer.PlayerColor != null)
        {
            weaponHolder.AssignColor2Weapon(currentlyBuiltPlayer.PlayerColor);
        }
    }

    public void AddInputManager()
    {
        if (currentlyBuiltPlayer == null) return;

        // Check if the InputManager already exists to avoid duplicates
        if (currentlyBuiltPlayer.GetComponent<InputManager>() == null)
        {
            currentlyBuiltPlayer.gameObject.AddComponent<InputManager>();
        }
    }

    public Player BuildPlayer()
    {
        if (currentlyBuiltPlayer != null)
        {
            // Activate the GameObject when fully built
            currentlyBuiltPlayer.gameObject.SetActive(true);
        }
        return currentlyBuiltPlayer;
    }
}

