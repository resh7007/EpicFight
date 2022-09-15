using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelectManager : MonoBehaviour
{
    [SerializeField] private PlayerPrefabs _playerPrefabs;

    [SerializeField] private GameObject spawnPos1;
    [SerializeField] private GameObject spawnPos2;
    [SerializeField] private TMP_Text playerName1;
    [SerializeField] private TMP_Text playerName2;
    private GameObject model1;
    private GameObject model2;
    [SerializeField] private GameObject SelectionSquare1Prefab;
    [SerializeField] private GameObject SelectionSquare2Prefab;

    private GameObject SelectionSquare1;
    private GameObject SelectionSquare2;

    [SerializeField] private IconsManager _iconsManager;
    private List<Button> btns = new List<Button>();
    private int player1CurrBtnId=-1;
    private int player2CurrBtnId=-1;
    private int totalAvailablePlayersNumber = 5;
    private bool selected1;
    private bool selected2; 

    [SerializeField] private GameObject particle1;
    [SerializeField] private GameObject particle2;
    [SerializeField] private bool isPlayer1Chosen;
    [SerializeField] private bool isPlayer2Chosen;

    private void Start()
    {
        btns = _iconsManager.GetSelectionButtons();
    }

    private void Update()
    {
        Player1Inputs();
        Player2Inputs();

    }

    void Player1Inputs()
    {
        if (Input.GetAxis("Horizontal") > 0 && !selected1)
        {
            if (player1CurrBtnId < totalAvailablePlayersNumber)
            {
                selected1 = true;
                player1CurrBtnId++;
                SelectPlayer1(btns[player1CurrBtnId].gameObject);
                RemoveSelectedP1();

            }
        }
        if (Input.GetAxis("Horizontal") < 0  && !selected1)
        {
            if (player1CurrBtnId > 0)
            {
                selected1 = true;
                player1CurrBtnId--;
                SelectPlayer1(btns[player1CurrBtnId].gameObject);
                RemoveSelectedP1();
            }
        }
        if(Input.GetAxis("Horizontal") == 0 )
            selected1 = false;

        if (Input.GetButtonDown("Fire1"))
        {
            SelectionSquare1.transform.GetChild(0).gameObject.SetActive(true);
            model1.transform.GetChild(0).GetComponent<Animator>().SetTrigger("HeavyPunch");
            particle1.SetActive(false);
            isPlayer1Chosen = true;
            StartCoroutine(LoadTheGameLevel());

        }    
    }
    void Player2Inputs()
    {
        if (Input.GetAxis("HorizontalP2") > 0 && !selected2)
        {
            if (player2CurrBtnId < totalAvailablePlayersNumber)
            {
                selected2 = true;
                player2CurrBtnId++;
                SelectPlayer2(btns[player2CurrBtnId].gameObject);
                RemoveSelectedP2();

            }
        }
        if (Input.GetAxis("HorizontalP2") < 0  && !selected2)
        {
            if (player2CurrBtnId > 0)
            {
                selected2 = true;
                player2CurrBtnId--;
                SelectPlayer2(btns[player2CurrBtnId].gameObject);
                RemoveSelectedP2();
            }
        }
        if(Input.GetAxis("HorizontalP2") == 0 )
            selected2 = false;

        if (Input.GetButtonDown("Fire1P2"))
        {
            SelectionSquare2.transform.GetChild(0).gameObject.SetActive(true);
            model2.transform.GetChild(0).GetComponent<Animator>().SetTrigger("HeavyPunch");
            particle2.SetActive(false);
            isPlayer2Chosen = true;
            StartCoroutine(LoadTheGameLevel());
        }    
    }
    void RemoveSelectedP1()
    {
        if(particle1.activeSelf) return;
        
        particle1.SetActive(true);
        SelectionSquare1.transform.GetChild(0).gameObject.SetActive(false);
        
        isPlayer1Chosen = false;

        
    }
    void RemoveSelectedP2()
    {
        if(particle2.activeSelf) return;
        
        particle2.SetActive(true);
        SelectionSquare2.transform.GetChild(0).gameObject.SetActive(false);
        isPlayer2Chosen = false;

    }
    public void SelectPlayer1(GameObject button)
    {
        int buttonId = GetButtonId(button);
        if(buttonId>totalAvailablePlayersNumber) return;

        if (SelectionSquare1 == null)
        {
            SelectionSquare1 = Instantiate(SelectionSquare1Prefab, button.transform.position, Quaternion.identity);
            SelectionSquare1.transform.SetParent(transform);
            
        }
        
        SelectionSquare1.transform.position = button.transform.position; 
        GameObject prefab =_playerPrefabs.playerPrefabs[buttonId];
        if(model1!=null)
            Destroy(model1);
        model1 = Instantiate(prefab, spawnPos1.transform.position, Quaternion.identity);
        playerName1.text = prefab.transform.GetChild(0).GetComponent<PlayerActions>().character.PlayerName;
        model1.GetComponent<React>().isStaticModel = true;
        model1.GetComponent<ModeState>().ModelStaticState(45);

    }
    public void SelectPlayer2(GameObject button)
    {
        int buttonId = GetButtonId(button);
        if(buttonId>totalAvailablePlayersNumber) return;

        if (SelectionSquare2 == null)
        {
            SelectionSquare2 = Instantiate(SelectionSquare2Prefab, button.transform.position, Quaternion.identity);
            SelectionSquare2.transform.SetParent(transform);
   
        }

        SelectionSquare2.transform.position = button.transform.position; 

        GameObject prefab =_playerPrefabs.playerPrefabs[buttonId];
        if(model2!=null)
            Destroy(model2);
        model2 = Instantiate(prefab, spawnPos2.transform.position, Quaternion.identity);
        playerName2.text = prefab.transform.GetChild(0).GetComponent<PlayerActions>().character.PlayerName;
        model2.GetComponent<React>().isStaticModel = true;
        model2.GetComponent<ModeState>().ModelStaticState(90);

    }

    private int GetButtonId(GameObject button)
    {
       return button.GetComponent<PlayerSelectButton>().buttonId;
    }

    IEnumerator LoadTheGameLevel()
    {
        yield return new WaitForSeconds(2.7f);
        Save.chosenPlayer1ID =  model1.transform.GetChild(0).GetComponent<PlayerActions>().character.Id;
        Save.chosenPlayer2ID =  model2.transform.GetChild(0).GetComponent<PlayerActions>().character.Id;

        if(isPlayer1Chosen && isPlayer2Chosen)
             SceneManager.LoadScene("Level1");
        
    }
}
