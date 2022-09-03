using System;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    private CharacterInput characterInput;

    private void Awake()
    {
        characterInput = GetComponent<CharacterInput>();
    }

 
    void Update()
    {
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(transform.position);
        if (ScreenBounds.x > Screen.width)
            characterInput.CanWalkRight = false;
        else if (ScreenBounds.x < 0)
            characterInput.CanWalkLeft = false;
        else
        {
            characterInput.CanWalkRight = true;
            characterInput.CanWalkLeft = true;
        }
    }
}
