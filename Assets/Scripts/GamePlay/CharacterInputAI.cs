using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputAI : CharacterInput
{
    private float OppDistance;
    public float AttackDistance = 1.5f;
    private bool MoveAI = true;
    private bool isInAttackState = false;
    private GameObject Opponent;
    private GameObject Player;
    private PlayerMovement playerMovement;

    protected override void Awake()
    {
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void SetOpponent(GameObject _opponent,GameObject _player)
    {
        Opponent = _opponent;
        Player = _player;
    }

    protected override void Update()
    {         
        animatorStateInfo = Anim.GetCurrentAnimatorStateInfo(0);
        FindDistanceToOpponent();
        WalkingLeftRight();
        JumpingCrouching();
        CheckIfKnockedOut();

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
                if (!CanWalkRight) return;
                if(!MoveAI) return;

                MoveAI = false;
                Anim.SetBool("Forward",false);
                Anim.SetBool("Backward",false);       
                isInAttackState = true;
                StartCoroutine(ForwardFalse());


            }
            else
            {
                if (playerMovement.dir<0)
                {
                    if(!MoveAI) return;
                    if (!CanWalkRight) return;
                    if (walkRight)
                    {
                        Anim.SetBool("Forward", true);
                        Anim.SetBool("Backward", false);
                        isInAttackState = false;
                        transform.Translate(WalkSpeed * Time.deltaTime, 0, 0);
                    }
                }
                if (playerMovement.dir>0)
                {
                    if(!MoveAI) return;
                    if (!CanWalkLeft) return;
                    if (walkLeft)
                    {
                        Anim.SetBool("Backward", true);
                        Anim.SetBool("Forward", false);
                        isInAttackState = false;

                        transform.Translate(-WalkSpeed * Time.deltaTime, 0, 0);
                    }

                }
            }
        }

       
        CheckIfBlock();

    }
  
    protected override void CheckIfKnockedOut()
    {
        if (Save.Player2Health <= 0)
        { 
            transform.GetChild(0).GetComponent<ActionsInput2>().enabled = false;
             StartCoroutine(KnockedOut());
         


        }
        if (Save.Player1Health <= 0)
        {
            StartCoroutine(VictoryCheer());
            transform.GetChild(0).GetComponent<ActionsInput2>().enabled = false;
            Anim.SetBool("Forward",false);
            Anim.SetBool("Backward",false);
        }
    }

    IEnumerator VictoryCheer()
    {
        transform.GetComponent<Character2Input>().enabled = false;
  

        yield return new WaitForSeconds(1.0f);
        Anim.SetTrigger("Victory");
    }

    IEnumerator KnockedOut()
    {

        yield return new WaitForSeconds(.1f);
        Anim.SetTrigger("KnockedOut");
        transform.GetComponent<Character2Input>().enabled = false;
        GetComponent<React>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        
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

    IEnumerator ForwardFalse()
    {
        yield return new WaitForSeconds(.6f);
        MoveAI = true;
    }
}
