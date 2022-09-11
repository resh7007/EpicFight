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
        int playerId = Save.TotalLevelWinnerID - 1;
        if (playerId == 0)
        {
            Vector3 pos = spawnPos.transform.position;
            pos.y += .66f;
            pos.z -= 1.5f;
            spawnPos.transform.position = pos;
        }

        player = Instantiate(_players.playerPrefabs[playerId], spawnPos.transform.position, Quaternion.identity);
        player.GetComponent<React>().isStaticModel = true;
        player.GetComponent<React>().enabled = false;

        player.GetComponent<BoxCollider>().isTrigger = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<BoxCollider>().enabled = false;
        player.GetComponent<CapsuleCollider>().enabled = false;
        MoveRestrict go= FindObjectOfType<MoveRestrict>();
        go.gameObject.SetActive(false);
        go.GetComponent<BoxCollider>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<ScreenBounds>().enabled = false;
        player.transform.GetChild(0).GetComponent<PlayerActions>().enabled = false;
        player.transform.rotation = Quaternion.Euler(0,90,0);
    }

    IEnumerator VictoryAnimation()
    {
        yield return new WaitForSeconds(1);
        player.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Victory");
    }
}
