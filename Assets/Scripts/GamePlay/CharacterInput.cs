using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour,ICharacterInput
{
    protected AnimatorStateInfo animatorStateInfo;
    protected Animator Anim;
    [SerializeField] protected bool _canWalkRight = true;
    [SerializeField] protected bool _canWalkLeft = true;
    [SerializeField] protected float WalkSpeed=3f;
    [SerializeField] protected bool IsJumping;
    [SerializeField] protected bool walkRight = true;
    [SerializeField] protected bool walkLeft = true;
    private bool isInBlock;
    private Rigidbody _rb;
    private Collider _boxCollider;
    private Collider _capsuleCollider; 
    protected virtual void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }
    protected virtual void Update()
    {
        animatorStateInfo = Anim.GetCurrentAnimatorStateInfo(0); 

        WalkingLeftRight();
        JumpingCrouching();
        CheckIfKnockedOut();
    }

    public bool GetIsInBlock()
    {
        return isInBlock;
    }

    protected virtual void CheckIfKnockedOut()
    {  
 
        if (Save.Player1Health <= 0)
        {
            transform.GetChild(0).GetComponent<ActionsInput>().enabled = false;
            StartCoroutine(KnockedOut());
        
        }

        if (Save.Player2Health <= 0)
        { 
            StartCoroutine(VictoryCheer());
            transform.GetChild(0).GetComponent<ActionsInput>().enabled = false;
            Anim.SetBool("Forward",false);
            Anim.SetBool("Backward",false);
        }
    }
    IEnumerator VictoryCheer()
    {
        transform.GetComponent<CharacterInput>().enabled = false; 

        yield return new WaitForSeconds(1.0f);

        Anim.SetTrigger("Victory");
    }
     IEnumerator KnockedOut()
     {
        yield return new WaitForSeconds(.1f);
        Anim.SetTrigger("KnockedOut");
        transform.GetComponent<CharacterInput>().enabled = false;
        GetComponent<React>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

    }

    public void SetWalkRight (bool value)
    {
        walkRight = value;
    }

    public void SetWalkLeft (bool value)
    {
        walkLeft = value;
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
            ResetTimeSlowMotion();
            if (Input.GetAxis("Horizontal")>0)
            { 
                if (!CanWalkRight) return;
                if (walkRight)
                {
                    Anim.SetBool("Forward", true);
                    transform.Translate(WalkSpeed * Time.deltaTime, 0, 0);
                }
            }
            if (Input.GetAxis("Horizontal")<0)
            {
               
                if (!CanWalkLeft) return;
                if (walkLeft)
                {
                    Anim.SetBool("Backward", true);
                    transform.Translate(-WalkSpeed * Time.deltaTime, 0, 0);
                }
            }
        }

    
        if (Input.GetAxis("Horizontal")==0)
        {
            Anim.SetBool("Forward",false);
            Anim.SetBool("Backward",false);
        }

        CheckIfBlock();
    }

    protected void ResetTimeSlowMotion()
    {
         Time.timeScale = 1;
    }

    protected void CheckIfBlock()
    {
        if (animatorStateInfo.IsTag("Block"))
        { 
            _rb.isKinematic = true;
            _boxCollider.enabled = false;
            _capsuleCollider.enabled = false;
            isInBlock = true;
        }
        else
        {
          
            _boxCollider.enabled = true;
            _capsuleCollider.enabled = true;
            _rb.isKinematic = false; 
            isInBlock = false;

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
