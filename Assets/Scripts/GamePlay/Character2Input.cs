using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Character2Input : CharacterInput
{
    protected override void WalkingLeftRight()
    {
   
        if (animatorStateInfo.IsTag("Motion"))
        {
            ResetTimeSlowMotion();

            if (Input.GetAxis("HorizontalP2")>0)
            {
                if (!GetCanWalkRight()) return;
                if (walkRight)
                {
                    Anim.SetBool("Forward", true); 
                    transform.Translate(WalkSpeed * Time.deltaTime, 0, 0);

                }
            }
            if (Input.GetAxis("HorizontalP2")<0) 
            {
                if (!GetCanWalkLeft()) return;
                if (walkLeft)
                {
                    Anim.SetBool("Backward", true);  
                    transform.Translate(-WalkSpeed * Time.deltaTime, 0, 0); 
                }

            }
        }

        if (Input.GetAxis("HorizontalP2")==0)
        {
            Anim.SetBool("Forward",false);
            Anim.SetBool("Backward",false);
        }
        CheckIfBlock();

    } 
    // public void Lose()
    // {
    //     ResetTimeSlowMotion();
    //     
    //     transform.GetChild(0).GetComponent<ActionsInput2>().enabled = false;
    //     StartCoroutine(KnockedOut());
    // }
    // public void Win()
    // {
    //     ResetTimeSlowMotion();
    //
    //     StartCoroutine(VictoryCheer());
    //     transform.GetChild(0).GetComponent<ActionsInput2>().enabled = false;
    //     Anim.SetBool("Forward", false);
    //     Anim.SetBool("Backward", false);
    // }
    // // IEnumerator VictoryCheer()
    // {
    //     transform.GetComponent<ICharacterInput>().enabled = false;
    //
    //
    //     yield return new WaitForSeconds(1.0f);
    //     Anim.SetTrigger("Victory");
    // }
    //
    // IEnumerator KnockedOut()
    // {
    //
    //     yield return new WaitForSeconds(.1f);
    //     Anim.SetTrigger("KnockedOut");
    //     transform.GetComponent<ICharacterInput>().enabled = false;
    //     GetComponent<React>().enabled = false;
    //     GetComponent<BoxCollider>().enabled = false;
    //     
    // }
    protected override void JumpingCrouching()
    {
        if (Input.GetAxis("VerticalP2") > 0 && !IsJumping)
        {
            Anim.SetTrigger("Jump");
            IsJumping = true;
            StartCoroutine(JumpPause());
        }

        if (Input.GetAxis("VerticalP2") < 0)
        {
            Anim.SetBool("Crouch", true);
        }

        if (Input.GetAxis("VerticalP2") == 0)
        {
            Anim.SetBool("Crouch", false);
        }
    }
}
