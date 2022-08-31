using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    [SerializeField] private Image Player1GreenHealthBar;
    [SerializeField] private Image Player1RedHealthBar;
    [SerializeField] private Image Player2GreenHealthBar;
    [SerializeField] private Image Player2RedHealthBar;
 
    void Update()
    {
        Player1GreenHealthBar.fillAmount = Save.Player1Health;
        Player2GreenHealthBar.fillAmount = Save.Player2Health;

        if (Save.Player2Timer > 0)
            Save.Player2Timer -= 2.0f *Time.deltaTime;
        
        if(Save.Player2Timer <=0)
            if (Player2RedHealthBar.fillAmount > Save.Player2Health)
            {
                Player2RedHealthBar.fillAmount -= 0.003f;
            }
        
        if (Save.Player1Timer > 0)
            Save.Player1Timer -= 2.0f *Time.deltaTime;
        
        if(Save.Player1Timer <=0)
            if (Player1RedHealthBar.fillAmount > Save.Player1Health)
            {
                Player1RedHealthBar.fillAmount -= 0.003f;
            }
    }
 
}
