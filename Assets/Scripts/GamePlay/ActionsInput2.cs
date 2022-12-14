using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsInput2 : ActionsInput
{
    protected override void StandingAttacks()
    {
        if (animatorStateInfo.IsTag("Motion"))
        {
            if (Input.GetButtonDown("Fire1P2"))
            {
                Anim.SetTrigger("LightPunch");
                isHit = false;
            }

            if (Input.GetButtonDown("Fire2P2"))
            {
                Anim.SetTrigger("HeavyPunch");
                isHit = false;
            }

            if (Input.GetButtonDown("Fire3P2"))
            {
                Anim.SetTrigger("LightKick");
                isHit = false;
            }

            if (Input.GetButtonDown("JumpP2"))
            {
                Anim.SetTrigger("HeavyKick");
                isHit = false;
            }

            if (Input.GetButtonDown("BlockP2"))
            {
                Anim.SetTrigger("BlockOn");
            }
        }

        if (animatorStateInfo.IsTag("Block"))
        {
            if (Input.GetButtonUp("BlockP2"))
            {
                Anim.SetTrigger("BlockOff");
            }

        }
    }

    protected override  void CrouchingAttack()
    {
        if (animatorStateInfo.IsTag("Crouching"))
        {
            if (Input.GetButtonDown("JumpP2"))
            {  
                Anim.SetTrigger("HeavyKick");
                isHit = false;
            }
       
            if (Input.GetButtonDown("Fire2P2"))
            { 
                Anim.SetTrigger("HeavyPunch");
                isHit = false;
            }
        }
    }

    protected override  void AerialMoves()
    {
        if (animatorStateInfo.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Fire3P2"))
            { 
                Anim.SetTrigger("LightKick");
                isHit = false;
            }  
        }
    }
}
