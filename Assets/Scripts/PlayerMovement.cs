using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator Anim;
    private GameObject PlayerGO;
    private GameObject Opponent;
    private Vector3 OppPosition;
    private bool FacingLeft = false;
    private bool FacingRight = true;
    public int dir;
    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        PlayerGO = transform.GetChild(0).gameObject;
        AssignAnOppponent();
    }

    void AssignAnOppponent()
    {
        if(transform.CompareTag("player1"))
            Opponent = GameObject.FindWithTag("player2");
        if(transform.CompareTag("player2"))
            Opponent = GameObject.FindWithTag("player1");

    }

    public Animator GetAnim()
    {
        return Anim;
    }

    public bool GetFacingLeft ()
    {
        return FacingLeft;
    }
    public bool GetFacingRight ()
    {
        return FacingRight;
    }
 

    void Update()
    {
        GetOpponentPosition();
    }

    void GetOpponentPosition()
    {
        OppPosition = Opponent.transform.position;
        FlipToFaceOpponent();
    }

    void FlipToFaceOpponent()
    {
   
       if (OppPosition.x > PlayerGO.transform.position.x)
       { 
           StartCoroutine(FaceLeft());
       }
       else if (OppPosition.x < PlayerGO.transform.position.x)
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
            dir = -1;
            yield return new WaitForSeconds(.15f);
            PlayerGO.transform.Rotate(0, -180, 0);
            Anim.SetLayerWeight(1, 0);
            
        }
    }
    IEnumerator FaceRight()
    {
        if (FacingRight)
        { 
            FacingLeft = true;
            FacingRight = false;
            dir = 1;
            yield return new WaitForSeconds(.15f);
            PlayerGO.transform.Rotate(0, 180, 0);
            Anim.SetLayerWeight(1, 1);

        }
    }
}
