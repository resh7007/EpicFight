using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public void SetPlayerMovement(PlayerMovement _playerMovement)
    {
        playerMovement = _playerMovement;
    }
 
    void Update()
    {
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);
        if (ScreenBounds.x > Screen.width)
            playerMovement.CanWalkRight = false;
        else if (ScreenBounds.x < 0)
            playerMovement.CanWalkLeft = false;
        else
        {
            playerMovement.CanWalkRight = true;
            playerMovement.CanWalkLeft = true;
        }
    }
}
