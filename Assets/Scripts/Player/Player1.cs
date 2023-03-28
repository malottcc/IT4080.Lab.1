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
    public class Player1 : NetworkBehaviour
    {
        public float speed = 6.0f;
        public It4080.ScoreChange scorechange;
        private Camera camera;

        public NetworkVariable<int> networkScore = new NetworkVariable<int>(10);

        void Start()
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }

        //-----------------------------
        //Update

        void Update()
        {
            if (!IsOwner) return;

            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(xInput, 0, yInput).normalized;
            transform.Translate(speed * Time.deltaTime * moveDirection);

            if (IsOwner)
            {
                //PlayerMovementServerRpc(moveDirection);
            }
        }

        //-------------------------------
        //Network Spawn

        public override void OnNetworkSpawn()
        {
            camera = transform.Find("Camera").GetComponent<Camera>();
            camera.enabled = IsOwner;

            networkScore.OnValueChanged += OnScoreChanged;

            UpdateScoreDisplay();
        }

        //------------------------------
        //Server Move Players

        [ServerRpc]
        public void PlayerMovementServerRpc(Vector3 posChange, ServerRpcParams rpcParams = default)
        {
            transform.Translate(posChange);
        }


        //----------------------------------
        //PowerUpTrigger

        private void HostHandlePowerUpPickUp(Collider power)
        {
            Destroy(power.gameObject);
        }

        public void OnTriggerEnter(Collider collision)
        {
            if (IsOwner || !IsOwner)
            {
                if (collision.gameObject.CompareTag("PowerUp"))
                {
                    HostHandlePowerUpPickUp(collision);
                }
            }
        }

        //---------------------------------
        //OnCollisionEnter Score

       void OnCollisionEnter(Collision collision)
        {
            if (IsServer)
            {
                if (collision.gameObject.CompareTag("Bullet"))
                {
                    ServerHandleBulletCollision(collision.gameObject);
                }
            }
        }

        private void ServerHandleBulletCollision(GameObject destroyBullet)
        {
            Debug.Log("Hit");
            Bullet getBullet = destroyBullet.GetComponent<Bullet>();
            networkScore.Value -= 1;
            Destroy(destroyBullet);
        }

        private void OnScoreChanged(int prevScore, int curScore)
        {
            UpdateScoreDisplay();
        }

        private void UpdateScoreDisplay()
        {
            if (IsOwner)
            {
                Debug.Log($"[{NetworkManager.Singleton.LocalClientId}] My Score = {networkScore.Value}");
            }
        }

    }
}
