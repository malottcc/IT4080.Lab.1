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

    private void SpawnAllPlayers()
    {
        
    }

    void Start()
    {
        
    }

    
}
