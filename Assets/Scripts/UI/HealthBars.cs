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
    
}
