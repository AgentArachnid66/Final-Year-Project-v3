using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Collider))]
public abstract class Pickup : MonoBehaviour
{
    protected Player playerRef;
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        playerRef = other.GetComponent<Player>();
        if (!ReferenceEquals(playerRef, null))
        {
            Debug.Log($"Collided with {other.name}");
            Activate();
            Despawn();
        }
    }

    public abstract void Activate();

    public abstract void Despawn();
}

