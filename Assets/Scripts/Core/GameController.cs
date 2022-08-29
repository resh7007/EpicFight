using UnityEngine;

public class GameController : MonoBehaviour
{ 
    public PlayerMovement playerMovement;

    private void Awake()
    {
        FindObjectOfType<ScreenBounds>().SetPlayerMovement(playerMovement);
        FindObjectOfType<PlayerActions>().SetPlayerMovement(playerMovement);
        FindObjectOfType<React>().SetPlayerMovement(playerMovement);

    }
 
}
