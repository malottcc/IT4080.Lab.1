using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Numerics;
using System;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;


public class PowerUpSpawner : NetworkBehaviour
{
    /*
    public GameObject powerUp;

    public bool spawnOnLoad = true;
    public float refreshTime = 2f;

    private Transform spawnPointTransform;

    private Rigidbody curPowerUp = null;

    public void Start()
    {
        spawnPointTransform = transform.Find("SpawnPoint");
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

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
        UnityEngine.Vector3 spawnPosition = transform.position;
        spawnPosition.y = 1;
        Rigidbody power = Instantiate(powerUp, spawnPosition, UnityEngine.Quaternion.identity);
        power.GetComponent<NetworkObject>().Spawn();

        curPowerUp = power;
    }
    */
}
