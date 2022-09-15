using System.Collections;
using UnityEngine;

public class LevelWinner : MonoBehaviour
{
    [SerializeField]private PlayerPrefabs _players;
    public GameObject spawnPos;
    private GameObject player;
    void Start()
    {
        SpawnPlayerModel();
        StartCoroutine(VictoryAnimation());
    }

    void SpawnPlayerModel()
    {
        int playerId = Save.TotalLevelWinnerID;
        if (playerId == 0)
        {
            Vector3 pos = spawnPos.transform.position;
            pos.y += .66f;
            pos.z -= 1.5f;
            spawnPos.transform.position = pos;
        }

        if (playerId == 2)
        {
            Vector3 pos = spawnPos.transform.position;
            pos.x = 3.528f;
            pos.y = 0.584f;
            pos.z = -7.549f;
            spawnPos.transform.position = pos;
           
        }

        player = Instantiate(_players.playerPrefabs[playerId], spawnPos.transform.position, Quaternion.identity);
        player.GetComponent<React>().isStaticModel = true;
        player.GetComponent<ModeState>().ModelStaticState(90); 
    }
 
    IEnumerator VictoryAnimation()
    {
        yield return new WaitForSeconds(1);
        player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Victory");
    }
}
