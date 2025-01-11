using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroupLogic : MonoBehaviour
{
    
    private Button[] buttons;
    private int selectedButtonIndex = -1;
    [SerializeField] GameObjectInformationContainer menuContainer;

    void Start()
    {
        // Get all buttons in the group
        buttons = GetComponentsInChildren<Button>();

        // Add click listeners to each button
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i; // Preserve the index for the lambda
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
        
        menuContainer.ContentTag = gameObject.name;
        // Optionally select the first button by default
        if (buttons.Length > 0)
        {
            SetChosenWeapon(0);
            SelectButton(0);
        }
        
    }

    void OnButtonClick(int index)
    {
        SetChosenWeapon(index);
        SelectButton(index);
        
    }

    void SetChosenWeapon(int index)
    {
        WeaponButton weaponButton = buttons[index].GetComponent<WeaponButton>();
        
        if (weaponButton != null && weaponButton.GetWeaponPrefab() != null)
        {
            
            menuContainer.Content = weaponButton.GetWeaponPrefab(); 
        }
        
    }

    string GetButtonText(int index)
    {
        return buttons[index].GetComponentInChildren<TextMeshProUGUI>().text;
    }
    void SelectButton(int index)
    {

        // Deselect the previously selected button
        if (selectedButtonIndex >= 0 && selectedButtonIndex < buttons.Length)
        {
            buttons[selectedButtonIndex].interactable = true;
        }

        // Select the new button
        selectedButtonIndex = index;
        buttons[selectedButtonIndex].interactable = false;  // This makes it use the "disabled" color state which we'll use as our "selected" state
    }

    // Optional: method to get currently selected button index
    public int GetSelectedButtonIndex()
    {
        return selectedButtonIndex;
    }
}
