using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputAI : CharacterInput
{
    private float OppDistance;
    public float AttackDistance = 2.5f;
    [SerializeField]private bool MoveAI = true;
    private bool isInAttackRange = false;
    private GameObject Opponent;
    private GameObject Player;
    private PlayerMovement playerMovement;
    private React _react;
    [SerializeField] private bool isBlocking = false;
    private IActionsInput actionsInput;
    protected override void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>(); 
        _react = GetComponent<React>(); 
    }

    public void SetOpponent(GameObject _opponent,GameObject _player)
    {
        Opponent = _opponent;
        Player = _player;
        actionsInput = transform.GetChild(0).GetComponent<ActionsInputAI>();
    }

    protected override void Update()
    {         
        animatorStateInfo = Anim.GetCurrentAnimatorStateInfo(0);
        WalkSpeed = _playerActions.GetFlyingJump() ? JumpSpeed : MoveSpeed;

        FindDistanceToOpponent();
        WalkingLeftRight();
        JumpingCrouching(); 

    }

    void FindDistanceToOpponent()
    {
        OppDistance = Vector3.Distance(Opponent.transform.position, Player.transform.position);
    }
 
    protected override void WalkingLeftRight()
    {
        if (animatorStateInfo.IsTag("Motion"))
        {
            ResetTimeSlowMotion();

            if (AttackDistance > OppDistance)
            {
                if (!GetCanWalkRight()) return;
                if(!MoveAI) return;

                MoveAI = false;
                Anim.SetBool("Forward",false);
                Anim.SetBool("Backward",false);       
                isInAttackRange = true;
                StartCoroutine(ForwardFalse());


            }
            else
            {
                if (playerMovement.dir<0)
                {
                    if(!MoveAI) return;
                    if (!GetCanWalkRight()) return;
                    if (walkRight)
                    {
                        Anim.SetBool("Forward", true);
                        Anim.SetBool("Backward", false);
                        isInAttackRange = false;
                        transform.Translate(WalkSpeed * Time.deltaTime, 0, 0);
                        
                    }
                }
                if (playerMovement.dir>0)
                {
                    if(!MoveAI) return;
                    if (!GetCanWalkLeft()) return;
                    if (walkLeft)
                    {
                        Anim.SetBool("Backward", true);
                        Anim.SetBool("Forward", false);
                        isInAttackRange = false;

                        transform.Translate(-WalkSpeed * Time.deltaTime, 0, 0);
                    }

                }
            }
        }

       
        CheckIfBlock();

    } 
    protected override void JumpingCrouching()
    {

        if (_react.GetDefend()==2)
        {
            if (isBlocking) return;
            actionsInput.SetInAttackState();
            Anim.SetTrigger("BlockOn");
            isBlocking = true;
            StartCoroutine(EndBlock());
        }
        
        if (_react.GetDefend()==3)
        { 
            actionsInput.SetInAttackState();

            Anim.SetBool("Crouch", true);
             _react.SetDefend(0); 
        }
        if (_react.GetDefend() == 4)
        {
            Anim.SetTrigger("Jump");
            _react.SetDefend(0); 

        }
  
    }

    IEnumerator EndBlock()
    {
        yield return new WaitForSeconds(1);
        Anim.SetTrigger("BlockOff");
        isBlocking = false;
        _react.SetDefend(0);

    }

    IEnumerator ForwardFalse()
    {
        yield return new WaitForSeconds(.6f);
        MoveAI = true;
    }

    public bool GetIsInAttackRange()
    {
        return isInAttackRange;

    }
   
}
