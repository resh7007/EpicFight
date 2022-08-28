using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private Animator Anim;

    [SerializeField] private float JumpSpeed;
    [SerializeField] private GameObject Player1;
    private AnimatorStateInfo animatorStateInfo;

    void Start()
    {
        Anim = GetComponent<Animator>();

    }

    private void Update()
    {
        animatorStateInfo = Anim.GetCurrentAnimatorStateInfo(0);
        StandingAttacks();
        CrouchingAttack();
        AerialMoves();
    }

    private void StandingAttacks()
    {
        if (animatorStateInfo.IsTag("Motion"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Anim.SetTrigger("LightPunch");

            }

            if (Input.GetButtonDown("Fire2"))
            {
                Anim.SetTrigger("HeavyPunch");
            }

            if (Input.GetButtonDown("Fire3"))
            {
                Anim.SetTrigger("LightKick");

            }

            if (Input.GetButtonDown("Jump"))
            {
                Anim.SetTrigger("HeavyKick");
            }
        }
    }

    private void CrouchingAttack()
    {
        if (animatorStateInfo.IsTag("Crouching"))
        {
            if (Input.GetButtonDown("Jump"))
            { 
                Anim.SetTrigger("HeavyKick");
            }
        }
    }

    private void AerialMoves()
    {
        if (animatorStateInfo.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Fire3"))
            { 
                Anim.SetTrigger("LightKick");
            }  
        }
    }

    public void JumpUp()
    {
        Player1.transform.Translate(0,JumpSpeed,0);
    }
    public void FlipUp()
    {
        Player1.transform.Translate(0,JumpSpeed,0);
        Player1.transform.Translate(0.01f,0,0);
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0,JumpSpeed,0);
        Player1.transform.Translate(-0.01f,0,0);
    }
}
