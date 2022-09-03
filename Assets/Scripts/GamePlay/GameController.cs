using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform SpawnPosPlayer1;
    public Transform SpawnPosPlayer2;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    private GameObject Player1;
    private GameObject Player2;
    public CharacterInput _CharacterInput;
    public Character2Input _Character2Input;
    public ActionsInput _ActionsInput;
    public ActionsInput2 _ActionsInput2;
    
    void Awake()
    {
        SpawnPlayers();
        SetPlayerScripts();
        SetMoveRestrictPlayers();
        SetPlayerOpponents();
    }

    void SpawnPlayers()
    { 
        Player1 = Instantiate(Player2Prefab, SpawnPosPlayer1.transform.position, Quaternion.identity);
        Player2 = Instantiate(Player1Prefab, SpawnPosPlayer2.transform.position, Quaternion.identity);
    }

    void SetPlayerScripts()
    {
        Player1.AddComponent<CharacterInput>();
        Player2.AddComponent<Character2Input>();
        Player1.transform.GetChild(0).gameObject.AddComponent<ActionsInput>();
        Player2.transform.GetChild(0).gameObject.AddComponent<ActionsInput2>();
    }

    void SetMoveRestrictPlayers()
    {
        PlayerMovement p1PlayerMovement = Player1.GetComponent<PlayerMovement>();
        PlayerMovement p2PlayerMovement = Player2.GetComponent<PlayerMovement>();
        p1PlayerMovement.GetMoveRestrict().SetPlayer(Player1);
        p2PlayerMovement.GetMoveRestrict().SetPlayer(Player2);

    }

    void SetPlayerOpponents()
    {
        PlayerMovement p1PlayerMovement = Player1.GetComponent<PlayerMovement>();
        PlayerMovement p2PlayerMovement = Player2.GetComponent<PlayerMovement>();

        p1PlayerMovement.AssignAnOppponent(Player2);
        p2PlayerMovement.AssignAnOppponent(Player1);

        ParticleSystem[] temp = {p2PlayerMovement.heavyFistParticle,p2PlayerMovement.heavyKickParticle,p2PlayerMovement.lightKickParticle};
        p2PlayerMovement.AssignParticles(p1PlayerMovement.heavyFistParticle,p1PlayerMovement.heavyKickParticle,p1PlayerMovement.lightKickParticle);
        p1PlayerMovement.AssignParticles(temp[0],temp[1],temp[2]);
    }

}
 
