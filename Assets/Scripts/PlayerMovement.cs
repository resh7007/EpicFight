using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator Anim;

    [SerializeField] private float WalkSpeed;
    [SerializeField] private bool IsJumping;

    private AnimatorStateInfo animatorStateInfo;
    [SerializeField] private bool _canWalkRight = true;
    [SerializeField] private bool _canWalkLeft = true;

    public GameObject Player1;
    public GameObject Opponent;
    private Vector3 OppPosition;
    private bool FacingLeft = false;
    private bool FacingRight = true;

    public bool GetFacingLeft ()
    {
        return FacingLeft;
    }
    public bool GetFacingRight ()
    {
        return FacingRight;
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

    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        
    }
 
    void Update()
    {
        animatorStateInfo = Anim.GetCurrentAnimatorStateInfo(0);
        GetOpponentPosition();
        WalkingLeftRight();
        JumpingCrouching();
    }

    void GetOpponentPosition()
    {
        OppPosition = Opponent.transform.position;
        FlipToFaceOpponent();
    }

    void FlipToFaceOpponent()
    {
   
       if (OppPosition.x > Player1.transform.position.x)
       { 
           StartCoroutine(FaceLeft());
       }
       else if (OppPosition.x < Player1.transform.position.x)
       { 
           StartCoroutine(FaceRight());
       }
 
    }

    
    IEnumerator FaceLeft()
    {
        if (FacingLeft)
        {
            FacingLeft = false;
            FacingRight = true;
            yield return new WaitForSeconds(.15f);
            Player1.transform.Rotate(0, -180, 0);
            Anim.SetLayerWeight(1, 0);
            
        }
    }
    IEnumerator FaceRight()
    {
        if (FacingRight)
        { 
            FacingLeft = true;
            FacingRight = false;
            yield return new WaitForSeconds(.15f);
            Player1.transform.Rotate(0, 180, 0);
            Anim.SetLayerWeight(1, 1);

        }
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
