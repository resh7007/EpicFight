using System.Collections; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterInput : MonoBehaviour, ICharacterInput
{
    protected AnimatorStateInfo animatorStateInfo;
    protected Animator Anim;
    [SerializeField] protected bool _canWalkRight = true;
    [SerializeField] protected bool _canWalkLeft = true;
    [SerializeField] protected float WalkSpeed = 3f;
    [SerializeField] protected bool IsJumping;
    [SerializeField] protected bool walkRight = true;
    [SerializeField] protected bool walkLeft = true;
    private bool isInBlock;
    protected Rigidbody _rb;
    protected Collider _boxCollider;
    protected Collider _capsuleCollider;
    [SerializeField]protected float JumpSpeed =12f;
    protected float MoveSpeed;
    protected PlayerActions _playerActions;
    protected PlayerMovement _playerMovement;
    protected virtual void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerActions = GetComponentInChildren<PlayerActions>();
        WalkSpeed = _playerActions.character.WalkSpeed;
        JumpSpeed = _playerActions.character.JumpSpeed;
        MoveSpeed = WalkSpeed;
    }

    protected virtual void Update()
    {
        animatorStateInfo = Anim.GetCurrentAnimatorStateInfo(0);

        if (Save.TimeOut)
        {
            Anim.SetBool("Forward",false);
            Anim.SetBool("Backward",false);
        }
        else
        {
            WalkSpeed = _playerActions.GetFlyingJump() ? JumpSpeed : MoveSpeed;

            WalkingLeftRight();
            JumpingCrouching(); 
        }

   
    }

    public bool GetIsInBlock()
    {
        return isInBlock;
    } 



    IEnumerator KnockedOut()
    {
        Anim.SetBool("KnockOut",true);

        yield return new WaitForSeconds(.1f);
        Anim.SetTrigger("KnockedOut");

        transform.GetComponent<ICharacterInput>().enabled = false;
        GetComponent<React>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

    }

    public void SetWalkRight(bool value)
    {
        walkRight = value;
    }

    public void SetWalkLeft(bool value)
    {
        walkLeft = value;
    }

    public bool GetCanWalkRight()
    {
        return _canWalkRight;
    }

    public bool GetCanWalkLeft()
    {
        return _canWalkLeft;
    }

    public void SetCanWalkRight(bool canWalk)
    {
        _canWalkRight = canWalk;
    }
    
    public void SetCanWalkLeft(bool canWalk)
    {
        _canWalkLeft = canWalk;
    }
 
    protected virtual void WalkingLeftRight()
    { 
        if (animatorStateInfo.IsTag("Motion"))
        {
            ResetTimeSlowMotion();
                if (Input.GetAxis("Horizontal")>0)
            { 
                if (!GetCanWalkRight()) return;
                if (walkRight)
                {
                    Anim.SetBool("Forward", true);
                     transform.Translate(WalkSpeed * Time.deltaTime, 0, 0);
                }
            }
            if (Input.GetAxis("Horizontal")<0)
            {
               
                if (!GetCanWalkLeft()) return;
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

    public void Lose()
    {
        ResetTimeSlowMotion();
        
        transform.GetChild(0).GetComponent<IActionsInput>().enabled = false;
        StartCoroutine(KnockedOut());
    }
 
    public IEnumerator Win()
    {
        ResetTimeSlowMotion();

        transform.GetChild(0).GetComponent<IActionsInput>().enabled = false;
        Anim.SetBool("Forward", false);
        Anim.SetBool("Backward", false); 
        StartCoroutine(VictoryCheer());
        yield return VictoryCheer(); 
    }

    IEnumerator VictoryCheer()
    {
        transform.GetComponent<ICharacterInput>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        transform.rotation = Quaternion.Euler(0,  _playerMovement.dir*-90, 0);
        Anim.SetTrigger("Victory");
        yield return new WaitForSeconds(3.0f);
        transform.rotation = Quaternion.Euler(0,0,0); 
    }

    public void ResetTimeSlowMotion()
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
        else  if (animatorStateInfo.IsTag("Motion"))
        {
            _boxCollider.enabled = true;
            _capsuleCollider.enabled = true;
            _rb.isKinematic = false; 
            isInBlock = false;
        }
        else if (animatorStateInfo.IsTag("Crouching") || animatorStateInfo.IsTag("Sweep"))
        {
            _boxCollider.enabled = false;

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

    public string GetPlayerName()
    {
        return _playerActions.character.PlayerName;
    }

   public void ResetPlayer()
    {
        Anim.SetBool("KnockOut",false); 
        transform.GetChild(0).GetComponent<IActionsInput>().enabled = true;
        GetComponent<ICharacterInput>().enabled = true;
    }
}
