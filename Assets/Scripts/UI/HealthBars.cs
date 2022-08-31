using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    [SerializeField] private Image Player1GreenHealthBar;
    [SerializeField] private Image Player2GreenHealthBar;
 
    void Update()
    {
        Player1GreenHealthBar.fillAmount = Save.Player1Health;
        Player2GreenHealthBar.fillAmount = Save.Player2Health;
    }
 
}
