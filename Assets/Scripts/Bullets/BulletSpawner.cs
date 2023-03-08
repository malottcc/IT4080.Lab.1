using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Unity.Netcode;
using System;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;

public class BulletSpawner: NetworkBehaviour
{
    [SerializeField]

    private GameObject bullet;

    [SerializeField]

    private Transform InistialTransform;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsOwner)
            {
                FireServerRpc(InistialTransform.position, InistialTransform.rotation);
            }
        }
    }
    

    [ServerRpc]
    public void FireServerRpc(Vector3 position, Quaternion rotation, ServerRpcParams rpcParams = default)
    {
        GameObject InstansiatedBullet = Instantiate(bullet, position, rotation);
        InstansiatedBullet.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
        Destroy(InstansiatedBullet, 3);
    }

}