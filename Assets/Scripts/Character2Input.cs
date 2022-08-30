using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Input : CharacterInput,ICharacterInput
{
    protected override void WalkingLeftRight()
    {
        if (animatorStateInfo.IsTag("Motion"))
        {
            if (Input.GetAxis("HorizontalP2")>0)
            {
                if (!CanWalkRight) return;
                if (walkRight)
                {
                    Anim.SetBool("Forward", true);
                    transform.Translate(WalkSpeed * Time.deltaTime, 0, 0);
                }
            }
            if (Input.GetAxis("HorizontalP2")<0)
            {
                if (!CanWalkLeft) return;
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

    }

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
