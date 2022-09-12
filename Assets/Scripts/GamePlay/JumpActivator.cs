using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpActivator : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement _playerMovement;
    private string opponentTag;
    [SerializeField] private float pushStrength = 1.3f;
    private void Awake()
    {
        player = transform.root.gameObject;
        _playerMovement=player.GetComponent<PlayerMovement>();
        if (player.transform.CompareTag("player1"))
            opponentTag = "SpaceDetector2";
        if (player.transform.CompareTag("player2"))
            opponentTag = "SpaceDetector1";
    }


    private void OnTriggerEnter(Collider other)
    {
        // if(other.gameObject.CompareTag("Untagged")) return;
        // if (other.gameObject.CompareTag(opponentTag))
        // { 
        //     player.transform.Translate( _playerMovement.dir*pushStrength,0,0);
        // }
    }
  

}
