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
    [SerializeField] private bool HeavyMoving = false;
    [SerializeField] private float PunchSlideAmount =.5f;
    private PlayerMovement playerMovement;
    private AudioSource MyPlayer;
 

    void Start()
    {
        Anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();

    }

    public void SetPlayerMovement(PlayerMovement _playerMovement)
    {
        playerMovement = _playerMovement;
    }

    private void Update()
    {
        HeavyPunchSlide();
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

            if (Input.GetButtonDown("Block"))
            {
                Anim.SetTrigger("BlockOn");
            }
        }

        if (animatorStateInfo.IsTag("Block"))
        {
            if (Input.GetButtonUp("Block"))
            {
                Anim.SetTrigger("BlockOff");
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
 
    void HeavyPunchSlide()
    {
        if (!HeavyMoving) return;
        
        if(playerMovement.GetFacingRight())
            Player1.transform.Translate(PunchSlideAmount*Time.deltaTime,0,0);
        if(playerMovement.GetFacingLeft())
            Player1.transform.Translate(-PunchSlideAmount*Time.deltaTime,0,0);
        
    }

    IEnumerator PunchSlide()
    {
        HeavyMoving = true;
        yield return new WaitForSeconds(.05f);
        HeavyMoving = false;
    }

    public void PunchWooshSound()
    {
        MyPlayer.clip = MyPlayer.GetComponent<AudioPlayer>().audioClip[0];
        MyPlayer.Play();
    }
    
    public void KickWooshSound()
    {
        MyPlayer.clip = MyPlayer.GetComponent<AudioPlayer>().audioClip[1];
        MyPlayer.Play();
    }

}
