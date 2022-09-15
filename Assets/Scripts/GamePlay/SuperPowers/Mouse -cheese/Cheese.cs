using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : SuperPower
{
    [SerializeField] private ParticleSystem cheeseExplosion;

    private void Start()
    {
        Destroy(gameObject,6);
    }
 

    void Update()
    { 
        transform.Rotate (Vector3.left *Time.deltaTime*400 , Space.World);
        transform.Translate(Vector3.left *Dir *Time.deltaTime *5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(opponent))
        {
            Instantiate(cheeseExplosion, other.ClosestPoint(transform.position), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
