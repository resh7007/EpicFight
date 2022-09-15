using System.Collections;
using UnityEngine;

public class SuperPowerManager : MonoBehaviour
{
    [SerializeField] private GameObject superPowerPrefab;
    [SerializeField] private Transform superPowerPos;
    [SerializeField] private ParticleSystem spawnParticle;
    public void SpawnSuperPower()
    {
        if (superPowerPrefab == null) return;
        if (superPowerPos == null) return;
        StartCoroutine(Throw());
    }

    IEnumerator Throw()
    {
        if(spawnParticle!=null)
             spawnParticle.Play();
        yield return new WaitForSeconds(0.5f);
       GameObject go = Instantiate(superPowerPrefab, superPowerPos.position, Quaternion.identity) as GameObject;
       go.GetComponent<SuperPower>().SetDir(GetComponent<PlayerMovement>().dir);
       go.GetComponent<SuperPower>().SetOpponent(GetComponent<PlayerMovement>().GetOpponentTag());
    }

}
