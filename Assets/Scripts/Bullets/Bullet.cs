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
        public int score = 0;
        public int currentScoreInt = 0;
        public string currentScoreString = "";
        public It4080.ScoreChange scorechange;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
                currentScoreInt = currentScoreInt + 1;
                currentScoreString = currentScoreInt.ToString();
                scorechange.ChangeTeamScoreServerRpc(currentScoreString);
            }
        }

    }
}