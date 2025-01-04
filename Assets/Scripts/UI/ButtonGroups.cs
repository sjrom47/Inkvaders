//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class ButtonGroups : MonoBehaviour
//{
//    [SerializeField] private int defaultSelectedIndex = 0;

//    private List<Button> buttons;
//    private List<TextMeshProUGUI> buttonTexts;
//    private int currentSelectedIndex = -1;

//    private void Awake()
//    {
//        // Get all buttons in this group
//        buttons = new List<Button>(GetComponentsInChildren<Button>());
//        buttonTexts = new List<TextMeshProUGUI>();

//        // Get TextMeshPro component from each button
//        foreach (Button button in buttons)
//        {
//            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
//            if (buttonText != null)
//            {
//                buttonTexts.Add(buttonText);
//            }
//            else
//            {
//                Debug.LogError($"Button {button.name} is missing TextMeshPro component!");
//            }
//        }

//        // Add click listeners to all buttons
//        for (int i = 0; i < buttons.Count; i++)
//        {
//            int index = i; // Create a local copy for the lambda
//            buttons[i].onClick.AddListener(() => OnButtonClick(index));
//        }

//        // Select default button
//        if (defaultSelectedIndex >= 0 && defaultSelectedIndex < buttons.Count)
//        {
//            SelectButton(defaultSelectedIndex);
//        }
//    }

//    private void OnButtonClick(int index)
//    {
//        SelectButton(index);
//    }

//    public void SelectButton(int index)
//    {
//        // Deselect current button if one is selected
//        if (currentSelectedIndex >= 0 && currentSelectedIndex < buttonTexts.Count)
//        {
//            buttonTexts[currentSelectedIndex].color = defaultColor;
//        }

//        // Select new button
//        if (index >= 0 && index < buttonTexts.Count)
//        {
//            buttonTexts[index].color = selectedColor;
//            currentSelectedIndex = index;
//        }

//        // You can add your custom logic here when a button is selected
//        OnButtonSelected(index);
//    }

//    protected virtual void OnButtonSelected(int index)
//    {
//        // Override this method in derived classes to handle selection events
//        Debug.Log($"Button {index} selected in group {gameObject.name}");
//    }

//    public int GetSelectedButtonIndex()
//    {
//        return currentSelectedIndex;
//    }
//}

