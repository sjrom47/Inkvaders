using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalTextShower : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winnerText;

    private void Awake()
    {
        winnerText.text = "";
        GameManager.Instance().ShowWinner += ShowWinner;
    }
    private void OnDisable()
    {
        GameManager.Instance().ShowWinner -= ShowWinner;
    }

    private void ShowWinner(string message)
    {
        winnerText.text = message;
    }
}
