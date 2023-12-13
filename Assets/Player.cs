using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private int health = 100;

    public void Damage(int damage)
    {
        health -= Mathf.Abs(damage);
        health = Mathf.Max(health, 0);

        if (health == 0)
            Die();
    }

    void Die()
    {
        NetworkServer.RemovePlayerForConnection(connectionToServer, true);
    }
}
