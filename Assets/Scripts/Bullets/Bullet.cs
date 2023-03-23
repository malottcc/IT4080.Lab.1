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
        ScoreChange scorechange = new ScoreChange();

       
        public void Start()
        {
            
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                Debug.Log("Adenture");
                scorechange.SetScore();
            }
        }
    }
}