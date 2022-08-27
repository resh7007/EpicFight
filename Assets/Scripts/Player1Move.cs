using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Anim;

    [SerializeField] private float WalkSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        WalkingLeftRight();

        JumpingCrouching();
    }

    void WalkingLeftRight()
    {
        if (Input.GetAxis("Horizontal")>0)
        {
            Anim.SetBool("Forward",true);
            transform.Translate(WalkSpeed,0,0);
        }
        if (Input.GetAxis("Horizontal")<0)
        {
            Anim.SetBool("Backward",true);
            transform.Translate(-WalkSpeed,0,0);

        }
        if (Input.GetAxis("Horizontal")==0)
        {
            Anim.SetBool("Forward",false);
            Anim.SetBool("Backward",false);
        }

    }

    void JumpingCrouching()
    {
        if (Input.GetAxis("Vertical")>0)
        {
            Anim.SetTrigger("Jump");
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
}
