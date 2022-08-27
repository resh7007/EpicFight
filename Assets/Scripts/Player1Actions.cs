using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    [SerializeField] private float JumpSpeed;
    [SerializeField] private GameObject Player1;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpUp()
    {
        Player1.transform.Translate(0,JumpSpeed,0);

    }
}
