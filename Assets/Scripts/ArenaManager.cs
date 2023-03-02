using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ArenaManager : NetworkBehaviour
{

    public Player1 playerPrefab;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer)
        {
            SpawnAllPlayers();
        }
    }

    private Player1 SpawnPlayerForClient(ulong clientId)
    {
        Vector3 spawnPosition = new Vector3(0, 2, clientId * 5);
        Player1 playerSpawn = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

        playerSpawn.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        return playerSpawn;
    }


    private void SpawnAllPlayers()
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            SpawnPlayerForClient(clientId);
        }
    }

    
    void Start()
    {
        
    }

    
}
