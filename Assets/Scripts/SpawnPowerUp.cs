using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Unity.Netcode;
using System.Net;
using System;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;


public class SpawnPowerUp : NetworkBehaviour
{
    public GameObject powerUp;
    public GameObject curSpawnPowerUp = null;
    public UnityEngine.Vector3 SpawnPosition;
    public bool spawnOnLoad = true;
    public float refreshTime = 2f;
    public float timeRemaining = 0f;

    public void Start()
    {
        SpawnPosition = gameObject.transform.position;
    }

    void Update()
    {
        if (IsServer)
        {
            if (timeRemaining > 0f)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining <= 0f)
                {
                    timeRemaining = 0;
                    SpawnPowerUpServerRpc();
                }
            } else if (curSpawnPowerUp == null)
            {
                timeRemaining = refreshTime;
            }
        }
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
    public void SpawnPowerUpServerRpc(ServerRpcParams rpcParams = default)
    {
        GameObject InstansiatedPowerUP = Instantiate(powerUp, SpawnPosition, UnityEngine.Quaternion.identity);
        InstansiatedPowerUP.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
        curSpawnPowerUp = InstansiatedPowerUP;
    }

}
