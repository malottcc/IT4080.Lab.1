using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PowerUpSpawner : NetworkBehaviour
{
    /*
    Rigidbody curPowerUp;
    public bool spawnOnLoad = true;
    public float refreshTime = 2f;

    public GameObject powerUp;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            HostOnNetworkSpawn();
        }
    }

    private void HostOnNetworkSpawn()
    {
        if (powerUp != null && spawnOnLoad)
        {
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = 1;
        Rigidbody power = Instantiate(powerUp, spawnPosition, Quaternion.identity);
        power.GetComponent<NetworkObject>().Spawn();
        curPowerUp = power;
    
    }
    */
}
