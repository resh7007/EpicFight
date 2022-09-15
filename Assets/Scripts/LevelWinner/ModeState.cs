using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeState : MonoBehaviour
{
    public void ModelStaticState(float _rotateDegree)
    {
        Transform player = transform;
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
        player.transform.rotation = Quaternion.Euler(0,_rotateDegree,0);
    
        player.GetComponent<React>().enabled = false;

    }
}
