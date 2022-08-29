using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator Anim;
    private GameObject Player1;
    public GameObject Opponent;
    private Vector3 OppPosition;
    private bool FacingLeft = false;
    private bool FacingRight = true;

    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
        Player1 = transform.GetChild(0).gameObject;
        Opponent = GameObject.Find("Cube");
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
}
