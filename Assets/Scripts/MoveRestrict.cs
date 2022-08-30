using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRestrict : MonoBehaviour
{
    private GameObject player;
    private ICharacterInput characterInput;
    private string opponentTag1;
    private string opponentTag2;

    private void Awake()
    {
        player = transform.parent.parent.parent.gameObject;
 
        characterInput=player.GetComponent<ICharacterInput>();
        if (player.transform.CompareTag("player1"))
        {
            opponentTag1 = "P2Left";
            opponentTag2 = "P2Right";

        }
        if (player.transform.CompareTag("player2"))  
        {
            opponentTag1 = "P1Left";
            opponentTag2 = "P1Right";

        }

        Debug.Log("opponentTag1="+opponentTag1);
        Debug.Log("opponentTag2="+opponentTag2);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(opponentTag1))
        {
            characterInput.SetWalkRight(false); 
        }
        if (other.gameObject.CompareTag(opponentTag2))
        {
            characterInput.SetWalkLeft(false); 


        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(opponentTag1))
        {
            characterInput.SetWalkRight(true);
        }
        if (other.gameObject.CompareTag(opponentTag2))
        {
            characterInput.SetWalkLeft(true);

        }

    }
}
