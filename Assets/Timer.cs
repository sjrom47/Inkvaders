using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer: MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;


    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

        }

        else if (remainingTime < 0)
        {
            remainingTime = 0;
            SceneManager.LoadScene("MenuScene");
            
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    
}