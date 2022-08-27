using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private float JumpSpeed;
    [SerializeField] private GameObject Player1;
    

  
    public void JumpUp()
    {
        Player1.transform.Translate(0,JumpSpeed  ,0);

    }
}
