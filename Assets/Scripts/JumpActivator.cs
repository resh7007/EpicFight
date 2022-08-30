using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpActivator : MonoBehaviour
{
    [SerializeField]private GameObject player;
    private PlayerMovement _playerMovement;
    private string opponentTag;
    private void Awake()
    {
        _playerMovement=player.GetComponent<PlayerMovement>();
        if (player.transform.CompareTag("player1"))
            opponentTag = "SpaceDetector2";
        if (player.transform.CompareTag("player2"))
            opponentTag = "SpaceDetector1";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(opponentTag))
        { 
            Debug.Log("opponentTag="+opponentTag);
            player.transform.Translate( _playerMovement.dir*.8f,0,0);
        }
    }
 
}
