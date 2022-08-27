using UnityEngine;

public class GameController : MonoBehaviour
{ 
    public PlayerMovement playerMovement;

    private void Awake()
    {
        FindObjectOfType<ScreenBounds>().SetPlayerMovement(playerMovement);
    }
 
}
