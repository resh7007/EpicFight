using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text WinText; 
    private GameObject player1;
    private GameObject player2;
    private ICharacterInput _characterInput1;
    private ICharacterInput _characterInput2;
    private bool called = false;
    private HealthBars _healthBars;
    private bool TimeIsUp = false;
    private Round _round;
    private int RoundNum=1;
    private void Awake()
    {
        _round = FindObjectOfType<Round>();
    }

    public void SetPlayers(GameObject _player1,GameObject _player2)
    {
        player1 = _player1;
        player2 = _player2;

        _characterInput1= player1.GetComponent<ICharacterInput>();
        _characterInput2= player2.GetComponent<ICharacterInput>();

        _healthBars = FindObjectOfType<HealthBars>();
        player1.GetComponent<PlayerMovement>().dir=-1;
    }

    void Update()
    {
        if(called) return;
        
        
        if (Save.Player1Health <= 0f )
        {
            called = true;
            _characterInput1.Lose();  

            Player2Wins();
            Save.TimeOut = true;
        }

        if (Save.Player2Health <= 0f)
        {
            called = true;
            _characterInput2.Lose();  

            Player1Wins();
            Save.TimeOut = true;

        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Save.Player1Wins = 2;
            SaveTotalWinnerAndLoadWinnerScene();
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            Save.Player2Wins = 2;
            SaveTotalWinnerAndLoadWinnerScene();
        }
    }

    public void ChoseTheWinner()
    {
        TimeIsUp = true;

        if(Save.Player1Health < Save.Player2Health )
            Player2Wins();
        else
        {
            Player1Wins();
        }
    }

    void Player1Wins()
    {
        Save.Player1Wins++;
        _round.SetPlayerWinSounds(1);
        ShowWinText(_characterInput1.GetPlayerName()); 

    }
    void Player2Wins()
    {
        Save.Player2Wins++; 
        _round.SetPlayerWinSounds(2);

        ShowWinText(_characterInput2.GetPlayerName());


    }
    void ShowWinText(string playerName)
    {
        StartCoroutine(NextRound(playerName));
    }
 

    IEnumerator NextRound(string playerName)
    { 
        _healthBars.ShowWinCounter();

        WinText.gameObject.SetActive(true);
        WinText.text = $"{playerName} Wins";
        if (TimeIsUp)
        {
            yield return new WaitForSeconds(2f);

            WinText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);

            StartCoroutine(ResetLevel());
        }
        else
        {
            if (Save.Player1Health <= 0f)
            {
              yield return _characterInput2.Win(); 
              WinText.gameObject.SetActive(false);

            }

            if (Save.Player2Health <= 0f)
            {
                yield return _characterInput1.Win(); 
                WinText.gameObject.SetActive(false); 

            } 

            StartCoroutine(ResetLevel());
        }
      
    }

    IEnumerator ResetLevel()
    {
        RoundNum++;
        if (Save.Player1Wins < 2 && Save.Player2Wins < 2)
        {
            
            _characterInput1.ResetPlayer();
            _characterInput2.ResetPlayer();
            _round.SetRoundNumber(RoundNum);
            Save.Player1Health = 1;
            Save.Player2Health = 1;
            _healthBars.ResetTimer();
            _healthBars.ResetHealthBar();
            yield return new WaitForSeconds(2);
 
            TimeIsUp = false;
            called = false;
        }
        else
        {

            SaveTotalWinnerAndLoadWinnerScene();             
        }
    }
 

 void SaveTotalWinnerAndLoadWinnerScene()
 {
     if (Save.Player1Wins > 1)
         Save.TotalLevelWinnerID = 1;
     else if (Save.Player2Wins > 1)
         Save.TotalLevelWinnerID = 2;

     SceneManager.LoadScene("Level1Winner");
 }
}
