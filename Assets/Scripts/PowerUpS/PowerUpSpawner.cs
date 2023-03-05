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
    private Transform spawnPointTransform;

    public void Start()
    {
        spawnPointTransform = transform.Find("PowerUpSpawnPoint");
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
            SpawnPowerUpServerRpc();
        }
    }

    [ServerRpc]
    private void SpawnPowerUpServerRpc(ServerRpcParams rpcParams = default)
    {
        UnityEngine.Vector3 spawnPosition = transform.position;
        spawnPosition.y = 3;
        GameObject InstansiatedPowerUP = Instantiate(powerUp, spawnPosition, UnityEngine.Quaternion.identity);
        InstansiatedPowerUP.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
    }
    
}
