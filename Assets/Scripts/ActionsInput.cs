using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsInput : MonoBehaviour
{
    protected AnimatorStateInfo animatorStateInfo;
    protected Animator Anim;
    protected bool isHit = false;

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

    }
    protected void Update()
    { 
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

}
