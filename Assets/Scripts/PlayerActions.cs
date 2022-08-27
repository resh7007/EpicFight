using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float JumpSpeed;
    [SerializeField] private GameObject Player1;
    

  
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
