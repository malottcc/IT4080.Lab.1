using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Unity.Netcode;
using System;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;
using TMPro;


namespace It4080
{
    public class BulletSpawner : NetworkBehaviour
    {
        [SerializeField]

        public GameObject bullet;

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

        void OnCollisionEnter(Collision collision)
        {
            
        }


        [ServerRpc]
        public void FireServerRpc(Vector3 position, Quaternion rotation, ServerRpcParams rpcParams = default)
        {
            GameObject InstansiatedBullet = Instantiate(bullet, position, rotation);
            InstansiatedBullet.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
            Destroy(InstansiatedBullet, 3);
        }

    }
}
