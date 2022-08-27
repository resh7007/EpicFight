using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private Animator Anim;

    [SerializeField] private float JumpSpeed;
    [SerializeField] private GameObject Player1;

    void Start()
    {
        Anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Anim.SetTrigger("LightPunch");
        }
    }

    public void JumpUp()
    {
        Player1.transform.Translate(0,JumpSpeed,0);
    }
    public void FlipUp()
    {
        Player1.transform.Translate(0,JumpSpeed,0);
        Player1.transform.Translate(0.01f,0,0);
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0,JumpSpeed,0);
        Player1.transform.Translate(-0.01f,0,0);
    }
}
