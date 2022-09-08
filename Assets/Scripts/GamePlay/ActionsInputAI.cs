using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ActionsInputAI : ActionsInput
{
    private CharacterInputAI characterInputAI;
   [SerializeField] private bool isInAttackState = false;
   [SerializeField] private bool isInCrouch = false;

   [SerializeField] private int AttackNumber = 0; 
    protected override void Update()
    {
        base.Update();
        if (isInCrouch) return;
        if(isInAttackState) return;
        if (characterInputAI.GetIsInAttackRange())
        {
            isInAttackState = true;
            RandomAttack();
            StartCoroutine(FinishAttack());
        }
    }

    IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(3.5f); 
        isInAttackState = false; 
    }

    public void SetCharacterInputAI(CharacterInputAI _characterInputAI)
    {
        characterInputAI = _characterInputAI;
    }
    private void RandomAttack()
    {
        AttackNumber = Random.Range(1, 5);
    }

    protected override void StandingAttacks()
    {
        if (animatorStateInfo.IsTag("Motion"))
        {
            if (AttackNumber ==1)
            {
                Anim.SetTrigger("LightPunch");
                isHit = false;
                AttackNumber = 0;
            }

            if (AttackNumber ==2)
            {
                Anim.SetTrigger("HeavyPunch");
                isHit = false;
                AttackNumber = 0;

            }

            if (AttackNumber ==3)
            {
                Anim.SetTrigger("LightKick");
                isHit = false;
                AttackNumber = 0;

            }

            if (AttackNumber ==4)
            {
                Anim.SetTrigger("HeavyKick");
                isHit = false;
                AttackNumber = 0;

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
                Anim.SetTrigger("HeavyKick");
                isHit = false;
                StartCoroutine(UnCrouch());
        }
    }

    IEnumerator UnCrouch()
    {
        yield return new WaitForSeconds(.4f);
        Anim.SetBool("Crouch",false);
        isInCrouch = false; 
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

    public override void SetInAttackState()
    {
        isInCrouch = true; 
    }
}
