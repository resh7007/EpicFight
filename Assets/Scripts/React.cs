using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class React : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator Anim;
    public void SetPlayerMovement(PlayerMovement _playerMovement)
    {
        playerMovement = _playerMovement;

    }

    private void Start()
    {
        Anim = playerMovement.GetAnim();

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        //Anim.SetTrigger("HeadReact");
        Anim.SetTrigger("BigReact");
    //    Anim.SetTrigger("KnockedOut");

    }
}
