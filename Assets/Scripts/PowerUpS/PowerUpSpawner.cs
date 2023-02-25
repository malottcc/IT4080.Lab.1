using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PowerUpSpawner : NetworkBehaviour
{

    public bool spawnOnLoad = true;
    public float refreshTime = 2f;

    public GameObject bonusPrefab;

    //public override OnNetworkSpawn()
    private void SpawnBonus()
    {

    }
}
