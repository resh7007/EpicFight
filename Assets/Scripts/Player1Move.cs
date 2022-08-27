using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Anim;

    [SerializeField] private float WalkSpeed;
    [SerializeField] private bool IsJumping;

    private AnimatorStateInfo animatorStateInfo;

    private bool CanWalkRight = true;
    private bool CanWalkLeft = true; 
 
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        
    }
 
    void Update()
    {
        animatorStateInfo = Anim.GetCurrentAnimatorStateInfo(0);

        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);

        if (ScreenBounds.x > Screen.width)
            CanWalkRight = false;

        else if (ScreenBounds.x < 0)
            CanWalkLeft = false;
        else
        {
            CanWalkRight = true;
            CanWalkLeft = true;
        }
        WalkingLeftRight();

        JumpingCrouching();
    }

    void WalkingLeftRight()
    {
        if (animatorStateInfo.IsTag("Motion"))
        {
            if (Input.GetAxis("Horizontal")>0)
            {
                if (!CanWalkRight) return;
                    
                Anim.SetBool("Forward",true);
                transform.Translate(WalkSpeed,0,0);
            }
            if (Input.GetAxis("Horizontal")<0)
            {
                if (!CanWalkLeft) return;
                Anim.SetBool("Backward",true);
                transform.Translate(-WalkSpeed,0,0);

            }
        }

    
        if (Input.GetAxis("Horizontal")==0)
        {
            Anim.SetBool("Forward",false);
            Anim.SetBool("Backward",false);
        }

    }

    void JumpingCrouching()
    {
        if (Input.GetAxis("Vertical")>0 && !IsJumping)
        {
           
                Anim.SetTrigger("Jump");
                IsJumping = true;
                StartCoroutine(JumpPause());
          

        }

        if (Input.GetAxis("Vertical") < 0)
        {
            Anim.SetBool("Crouch",true);
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            Anim.SetBool("Crouch",false);
        }
    }

    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
    }
}
