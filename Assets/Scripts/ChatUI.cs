using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System;
using System.Net;
using TMPro;

public class ChatUI : NetworkBehaviour
{
    public TMPro.TMP_Text txtChatLog;
    public Button btnSend;
    public TMPro.TMP_InputField inputMessage;

    public void Start()
    {
        btnSend.onClick.AddListener(ClientOnSendClicked);
        inputMessage.onSubmit.AddListener(ClientOnInputSubmit);
    }

    public override void OnNetworkSpawn()
    {
  
        if (IsHost)
        {
            SendChatMessageClientRpc("I am the host, Whoop Whoop!");
        }
        else
        {
            SendChatMessageServerRpc("I am a client, Yay!");
        }
    }

    private void SendUIMessage()
    {
        string msg = inputMessage.text;
        inputMessage.text = "";
        SendChatMessageServerRpc(msg);
    }


    //------
    //RPCs

    [ClientRpc]
    public void SendChatMessageClientRpc(string message, ClientRpcParams clientRpcParams = default)
    {
        Debug.Log(message);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SendChatMessageServerRpc(string message, ServerRpcParams serverRpcParams = default)
    {
        Debug.Log($"Host got message: {message}");
    }

    //-------------
    //Events

    public void ClientOnSendClicked()
    {
        SendUIMessage();
    }

    public void ClientOnInputSubmit(string text)
    {
        SendUIMessage();
    }



}
