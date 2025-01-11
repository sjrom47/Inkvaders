using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    public float TimeLeft { get; set; }
    public event Action OnGameEnd;
    void Awake()
    {
        GameManager.Instance().OnGameStart += OnGameStart;
    }

    void OnGameStart()
    {
        StartCoroutine(TimerCoroutine());
    }
    IEnumerator TimerCoroutine()
    {
       
        while (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;


            int minutes = Mathf.FloorToInt(TimeLeft / 60);
            int seconds = Mathf.FloorToInt(TimeLeft % 60);
            textMeshProUGUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return null;

        }
        TimeLeft = 0;
        OnGameEnd?.Invoke();

    }
    private void OnDestroy()
    {
        GameManager.Instance().OnGameStart -= OnGameStart;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
