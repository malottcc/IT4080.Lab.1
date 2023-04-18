using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Unity.Netcode;
using System;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;
using TMPro;

namespace It4080 { 

    public class Chat : NetworkBehaviour
    { 
        public const string MSG_SYSTEM = "SYSTEM";

        public class ChatMessage
        {
            public string to = null;
            public string from = null;
            public string message = null;
        }

        public TMPro.TMP_Text txtChatLog;
        public Button btnSend;
        public TMPro.TMP_InputField inputMessage;

        public event Action<ChatMessage> sendMessage;

        private ulong clientId = 0;

        public void Start() {

            btnSend.onClick.AddListener(BtnSendOnClick); //ClientOnSendClicked
            inputMessage.onSubmit.AddListener(InputMessageOnSubmit); //ClientOnInputSubmit
            txtChatLog.text = "Super Chat 64 Plus v2\n ";
        }

        public override void OnNetworkSpawn() {
            
            base.OnNetworkSpawn();
            enabled = true;
            SystemMessage("OnNetworkSpawn");
            clientId = NetworkManager.Singleton.LocalClientId;

            if (IsHost)
            {
                SendChatMessageClientRpc("I am the host, Whoop Whoop!", "MSG_SYSTEM");
            }
            else
            {
                SendChatMessageServerRpc("I am a new Client, Sup!");
            }

            txtChatLog.text = "     -- Started Chat Log --";

        }


        //------------------
        //RPCs

        [ClientRpc]
        public void SendChatMessageClientRpc(string message, string from, ClientRpcParams clientRpcParams = default)
        {
            ChatMessage msg = new ChatMessage();
            msg.message = message;
            msg.from = from;
            DisplayMessage(msg);
            Debug.Log(message);
        }

        [ServerRpc(RequireOwnership = false)]
        public void SendChatMessageServerRpc(string message, ServerRpcParams serverRpcParams = default)
        {
            
            Debug.Log($"Host got message: {message}");
            SendChatMessageClientRpc(message, serverRpcParams.Receive.SenderClientId.ToString());
        }

     

        private void DisplayMessage(ChatMessage msg)
        {

            txtChatLog.text += $"\n";

            string from = msg.from;
            if(from == NetworkManager.Singleton.LocalClientId.ToString())
            {
                from = "you";
            } 

            if(msg.from == MSG_SYSTEM)
            {
                txtChatLog.text += $"<<{from}>>{msg.message}\n";
            } else
            {
                txtChatLog.text += $"[{from}]: {msg.message}\n";
            }
            
        }

        //------------------------
        //Events

        private void SendMessage()
        {
            ChatMessage msg = new ChatMessage();
            msg.message = inputMessage.text;
            inputMessage.text = "";
            //sendMessage.Invoke(msg);
            SendChatMessageServerRpc(msg.message);
        }

        private void BtnSendOnClick()
        {
            SendMessage();
        }

        private void InputMessageOnSubmit(string text)
        {
            SendMessage();
        }


        private void OnEnable() {
            enable(true);
        }


        private void OnDisable() {
            enable(false);
        }


        // ----------------
        // Public
        // ----------------


        public void enable(bool should = true)
        {
            inputMessage.enabled = should;
            btnSend.enabled = should;
        }


        public void ShowMessage(ChatMessage msg)
        {
            DisplayMessage(msg);
        }


        public void SystemMessage(string text)
        {
            ChatMessage msg = new ChatMessage();
            msg.message = text;
            msg.from = MSG_SYSTEM;
            DisplayMessage(msg);
            Debug.Log($"system msg:  {text}");
            Debug.Log(txtChatLog.text);
        }
    }
}