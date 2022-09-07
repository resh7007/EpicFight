using System;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    private ICharacterInput characterInput; 
    private Vector3 pos;
    public void SetCharacterInput()
    {
        characterInput = GetComponent<ICharacterInput>();
        pos = gameObject.transform.position;
    }
    void Update()
    {
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(transform.position);
        
        if (ScreenBounds.x > Screen.width)
        {
            characterInput.SetCanWalkRight(false);
            MovePlayerAwayFromBounds(3.95f);
        }
        else if (ScreenBounds.x < 0)
        {
            characterInput.SetCanWalkLeft(false); 
          MovePlayerAwayFromBounds(-3.95f);
        }
        else
        {
            characterInput.SetCanWalkRight(true);
            characterInput.SetCanWalkLeft(true); 
        }
    }

    void MovePlayerAwayFromBounds(float posX)
    { 
      pos.x = posX;
      gameObject.transform.position = pos;
   
    }
    


}
