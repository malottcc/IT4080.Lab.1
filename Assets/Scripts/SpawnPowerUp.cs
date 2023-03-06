using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Unity.Netcode;
using System;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;


public class SpawnPowerUp : NetworkBehaviour
{
    public GameObject powerUp;
    public bool spawnOnLoad = true;
    public float refreshTime = 2f;
    public UnityEngine.Vector3 SpawnPosition;
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
                    SpawnPowerUpServerRpc(SpawnPosition);
                }
            } else if (powerUp == null)
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
            SpawnPowerUpServerRpc(SpawnPosition);
        }
    }

    [ServerRpc]
    public void SpawnPowerUpServerRpc(Vector3 spawnposition, ServerRpcParams rpcParams = default)
    {
        
        GameObject InstansiatedPowerUP = Instantiate(powerUp, spawnposition, UnityEngine.Quaternion.identity);
        InstansiatedPowerUP.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
    }

}
