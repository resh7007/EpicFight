using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform SpawnPosPlayer1;
    public Transform SpawnPosPlayer2;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    private GameObject Player1;
    private GameObject Player2;
    public bool isTwoPlayerGame;
    private PlayerMovement p1PlayerMovement;
    private PlayerMovement p2PlayerMovement; 

    void Awake()
    {
        SpawnPlayers();
        if (isTwoPlayerGame)
        {
            SetPlayerScripts();
            SetMoveRestrictPlayers();
            SetPlayerOpponents();
            SetPlayersParticles();
        }
        else
        { 
            SetPlayerAndAIScripts();
            SetMoveRestrictPlayers();
            SetPlayerOpponents();
            SetPlayersParticles();

        }
        SetupGameManager();

    }

    void SpawnPlayers()
    {
        SpawnPlayer1();
        SpawnPlayer2();
    }

    void SetupGameManager()
    {
        GetComponent<GameManager>().SetPlayers(Player1,Player2);
    }


    void SetPlayerScripts()
    {
        SetPlayer1Script();
        SetPlayer2Script();
    }
    void SetPlayerAndAIScripts()
    {
        SetPlayer1Script();
        SetAIScript();
    }
    
    void SetMoveRestrictPlayers()
    {
        SetMoveRestrictPlayer1();
        SetMoveRestrictPlayer2();
    }
    

    void SpawnPlayer1()
    {
        Player1 = Instantiate(Player2Prefab, SpawnPosPlayer1.transform.position, Quaternion.identity);
        Player1.tag = "player1";
    }
    void SpawnPlayer2()
    {
        Player2 = Instantiate(Player1Prefab, SpawnPosPlayer2.transform.position, Quaternion.identity);
        Player2.tag = "player2";
    }
 
    
    void SetPlayer1Script()
    {
        Player1.AddComponent<CharacterInput>();
        GameObject child =Player1.transform.GetChild(0).gameObject;
        child.AddComponent<ActionsInput>();
        Player1.GetComponent<ScreenBounds>().SetCharacterInput();  
    }
    void SetPlayer2Script()
    {
        Player2.AddComponent<Character2Input>();
        GameObject child =Player2.transform.GetChild(0).gameObject;

        child.AddComponent<ActionsInput2>();
        Player2.GetComponent<ScreenBounds>().SetCharacterInput();  



    }
    void SetAIScript()
    {
        Player2.AddComponent<CharacterInputAI>();
        GameObject child =Player2.transform.GetChild(0).gameObject;
        child.AddComponent<ActionsInputAI>();
        child.GetComponent<ActionsInputAI>().SetCharacterInputAI(Player2.GetComponent<CharacterInputAI>());

        Player2.GetComponent<ScreenBounds>().SetCharacterInput();

        Player2.GetComponent<CharacterInputAI>().SetOpponent(Player1,Player2);  
    }

    void SetMoveRestrictPlayer1()
    {
        p1PlayerMovement = Player1.GetComponent<PlayerMovement>();
        p1PlayerMovement.GetMoveRestrict().SetPlayer(Player1);
        p1PlayerMovement.LeftRestrict.tag = "P1Left";
        p1PlayerMovement.RightRestrict.tag = "P1Right";
    }
    void SetMoveRestrictPlayer2()
    {
        p2PlayerMovement = Player2.GetComponent<PlayerMovement>();
        p2PlayerMovement.GetMoveRestrict().SetPlayer(Player2);
        p2PlayerMovement.LeftRestrict.tag = "P2Left";
        p2PlayerMovement.RightRestrict.tag = "P2Right";
    }
    void SetPlayerOpponents()
    {
        p1PlayerMovement.AssignAnOppponent(Player2);
        p2PlayerMovement.AssignAnOppponent(Player1);
    }

    void SetPlayersParticles()
    {
        ParticleSystem[] temp = {p2PlayerMovement.heavyFistParticle,p2PlayerMovement.heavyKickParticle,p2PlayerMovement.lightKickParticle};
        p2PlayerMovement.AssignParticles(p1PlayerMovement.heavyFistParticle,p1PlayerMovement.heavyKickParticle,p1PlayerMovement.lightKickParticle);
        p1PlayerMovement.AssignParticles(temp[0],temp[1],temp[2]);
    }


}
 
