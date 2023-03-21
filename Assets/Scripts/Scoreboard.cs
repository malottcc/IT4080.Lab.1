using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Scoreboard : NetworkBehaviour
{

    public float playerCount;

    public void CountPlayerHitPoint()
    {
        playerCount += 1;
    }



}
