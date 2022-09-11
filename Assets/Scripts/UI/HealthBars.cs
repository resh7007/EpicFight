using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBars : MonoBehaviour
{
    [SerializeField] private Image Player1GreenHealthBar;
    [SerializeField] private Image Player1RedHealthBar;
    [SerializeField] private Image Player2GreenHealthBar;
    [SerializeField] private Image Player2RedHealthBar;
    [SerializeField] private Image P1Win1;
    [SerializeField] private Image P1Win2;
    [SerializeField] private Image P2Win1;
    [SerializeField] private Image P2Win2;

    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private float levelLasts = 90;
     private float LevelTime;

    private void Start()
    {
        Save.TimeOut = true;
        LevelTime = levelLasts;

    }


    

    void Update()
    {
   
        Player1GreenHealthBar.fillAmount = Save.Player1Health;
        Player2GreenHealthBar.fillAmount = Save.Player2Health;

        if (Save.Player2Timer > 0)
            Save.Player2Timer -= 2.0f *Time.deltaTime;
        
        if(Save.Player2Timer <=0)
            if (Player2RedHealthBar.fillAmount > Save.Player2Health)
            {
                Player2RedHealthBar.fillAmount -= 0.01f;
            }
        
        if (Save.Player1Timer > 0)
            Save.Player1Timer -= 2.0f *Time.deltaTime;
        
        if(Save.Player1Timer <=0)
            if (Player1RedHealthBar.fillAmount > Save.Player1Health)
            {
                Player1RedHealthBar.fillAmount -= 0.01f;
            }
        
        if(Save.TimeOut) return;
        
        StartTimer();
    }

    void StartTimer()
    {
        if (LevelTime > 0f)
            LevelTime -= Time.deltaTime;

        if (LevelTime <= 0.1f)
        {
            Save.TimeOut = true;
            FindObjectOfType<GameManager>().ChoseTheWinner();

        }

        TimerText.text = Mathf.Round(LevelTime).ToString(CultureInfo.InvariantCulture);
    }

    public void ResetTimer()
    {
        LevelTime = levelLasts; 

    }
    public void ResetHealthBar()
    {
        Player1RedHealthBar.fillAmount = 1;
        Player2RedHealthBar.fillAmount = 1;
    }
    public void ShowWinCounter()
    { 
        if (Save.Player1Wins == 1)
        {
            P1Win1.gameObject.SetActive(true);
        }
        else if (Save.Player1Wins > 1)
        {
            P1Win1.gameObject.SetActive(true);
            P1Win2.gameObject.SetActive(true);
        }
        if (Save.Player2Wins == 1)
        {
            P2Win1.gameObject.SetActive(true);
        }
        else if (Save.Player2Wins > 1)
        {
            P2Win1.gameObject.SetActive(true);
            P2Win2.gameObject.SetActive(true);
        }
    }
    
}
