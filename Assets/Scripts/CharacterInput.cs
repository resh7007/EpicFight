using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    protected AnimatorStateInfo animatorStateInfo;
    protected Animator Anim;
    [SerializeField] protected bool _canWalkRight = true;
    [SerializeField] protected bool _canWalkLeft = true;
    [SerializeField] protected float WalkSpeed=0.03f;
    [SerializeField] protected bool IsJumping;

    public void Awake()
    {
        Anim = GetComponentInChildren<Animator>(); 
    }
    public void Update()
    {
        animatorStateInfo = Anim.GetCurrentAnimatorStateInfo(0); 
        WalkingLeftRight();
        JumpingCrouching();
    }
    public bool CanWalkRight
    {
        get => _canWalkRight;
        set => _canWalkRight = value;
    }

    public bool CanWalkLeft
    {
        get => _canWalkLeft;
        set => _canWalkLeft = value;
    }

    protected virtual void WalkingLeftRight()
    {
        if (animatorStateInfo.IsTag("Motion"))
        {
            if (Input.GetAxis("Horizontal")>0)
            {
                if (!CanWalkRight) return;
                    
                Anim.SetBool("Forward",true);
                transform.Translate(WalkSpeed *Time.deltaTime,0,0);
            }
            if (Input.GetAxis("Horizontal")<0)
            {
                if (!CanWalkLeft) return;
                Anim.SetBool("Backward",true);
                transform.Translate(-WalkSpeed*Time.deltaTime,0,0);

            }
        }

    
        if (Input.GetAxis("Horizontal")==0)
        {
            Anim.SetBool("Forward",false);
            Anim.SetBool("Backward",false);
        }

    }

    protected virtual void JumpingCrouching()
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
    protected IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
    }
}
