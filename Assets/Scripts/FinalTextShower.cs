using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalTextShower : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winnerText;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance();
        winnerText.text = "";
        gameManager.ShowWinner += ShowWinner;
    }
    private void OnDisable()
    {
        if (gameManager != null)
        {
            gameManager.ShowWinner -= ShowWinner;
        }
    }

    private void ShowWinner(string message)
    {
        winnerText.text = message;
    }
}
