using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsInput : MonoBehaviour,IActionsInput
{
    protected AnimatorStateInfo animatorStateInfo;
    protected Animator Anim;
    protected bool isHit = false;
    protected SuperPowerManager SuperPowerManager;

    public void SetHit(bool flag)
    {
        isHit = flag;
    }
    public bool GetHit()
    {
        return isHit;
    }
    protected void Start()
    {
        Anim = transform.GetComponent<Animator>();
        SuperPowerManager = transform.root.GetComponent<SuperPowerManager>();
    }
    protected virtual void Update()
    { 
        if(Save.TimeOut) return;
        
        animatorStateInfo = Anim.GetCurrentAnimatorStateInfo(0);

        StandingAttacks();
        CrouchingAttack();
        AerialMoves();
    }

    protected virtual void StandingAttacks()
    {
        if (animatorStateInfo.IsTag("Motion"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Anim.SetTrigger("LightPunch");
                isHit = false;
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Anim.SetTrigger("HeavyPunch");
                isHit = false;
            }

            if (Input.GetButtonDown("Fire3"))
            {
                Anim.SetTrigger("LightKick");
                isHit = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                Anim.SetTrigger("HeavyKick");
                isHit = false;
            }

            if (Input.GetButtonDown("Block"))
            {
                Anim.SetTrigger("BlockOn");
            }
            
            if (Input.GetButtonDown("SuperPower1"))
            {
                Anim.SetTrigger("SuperPower1");
                SuperPowerManager.SpawnSuperPower();
                isHit = false;
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

    protected virtual  void CrouchingAttack()
    {
        if (animatorStateInfo.IsTag("Crouching"))
        {
            if (Input.GetButtonDown("Jump"))
            { 
                Anim.SetTrigger("HeavyKick"); 
                isHit = false;
            }
            if (Input.GetButtonDown("Fire2"))
            { 
                Anim.SetTrigger("HeavyPunch");
                isHit = false;
            }
        }
    }

    protected virtual  void AerialMoves()
    {
        if (animatorStateInfo.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Fire3"))
            { 
                Anim.SetTrigger("LightKick");
                isHit = false;
            }  
        }
    }

    public virtual void SetInAttackState()
    { 
    }
}
