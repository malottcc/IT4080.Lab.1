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
    public class Bullet : NetworkBehaviour
    {

        [SerializeField]
        private float speed = 20f;
        public GameObject curBullet;

        void OnCollisionEnter(Collision collision)
        {

        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
        }
    }
}