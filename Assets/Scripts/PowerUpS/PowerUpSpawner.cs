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

    public GameObject powerUp;
    public bool spawnOnLoad = true;
    public float refreshTime = 2f;
    public Transform SpawnPointValue; 

    public void Start()
    {
        SpawnPointValue = PowerUpSpawnPoint.position;
    }

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
            SpawnPowerUpServerRpc(powerUp, SpawnPointValue);
        }
    }

    [ServerRpc]
    private void SpawnPowerUpServerRpc(GameObject powerup, Transform SpawnPointValue, ServerRpcParams rpcParams = default)
    {
        UnityEngine.Vector3 spawnPosition = SpawnPointValue;
        spawnPosition.y = 3;
        GameObject InstansiatedPowerUP = Instantiate(powerup, spawnPosition, UnityEngine.Quaternion.identity);
        InstansiatedPowerUP.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
    }
    
}
