using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField]private ICharacterInput _characterInput1;
    [SerializeField]private ICharacterInput _characterInput2;
    private bool called = false;

    public void SetPlayers(GameObject _player1,GameObject _player2)
    {
        player1 = _player1;
        player2 = _player2;

        _characterInput1= player1.GetComponent<ICharacterInput>();
        _characterInput2= player2.GetComponent<ICharacterInput>();

    }

    void Update()
    {
        if(called) return;
        
        
        if (Save.Player1Health <= 0f )
        {
            called = true;
            _characterInput1.Lose();
            _characterInput2.Win();
        }

        if (Save.Player2Health <= 0f)
        {
            called = true;
            _characterInput1.Win();
            _characterInput2.Lose();
        }
    }
}
