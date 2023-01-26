using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;


public class Main : NetworkBehaviour
{

    public It4080.NetworkSettings netSettings;

    // Start is called before the first frame update ---------------
    void Start()
    {
        netSettings.startServer += NetSettingsOnServerStart;
        netSettings.startHost += NetSettingsOnHostStart;
        netSettings.startClient += NetSettingsOnClientStart;
    }

    private void StartClient(IPAddress ip, ushort port)
    {
        var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.ConnectionData.Address = ip.ToString();
        utp.ConnectionData.Port = port;

        NetworkManager.Singleton.StartClient();
        netSettings.hide();
        Debug.Log("start client");

    }

    private void StartHost(IPAddress ip, ushort port)
    {
        var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.ConnectionData.Address = ip.ToString();
        utp.ConnectionData.Port = port;

        NetworkManager.Singleton.StartHost();
        netSettings.hide();
        Debug.Log("start client");


    }

    private void StartServer(IPAddress ip, ushort port)
    {
        var utp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        utp.ConnectionData.Address = ip.ToString();
        utp.ConnectionData.Port = port;

        NetworkManager.Singleton.StartServer();
        netSettings.hide();
        Debug.Log("start client");
    }



    //Events ---------------

    private void NetSettingsOnServerStart(IPAddress ip, ushort port)
    {
        Debug.Log("start server");
    }

    private void NetSettingsOnHostStart(IPAddress ip, ushort port)
    {
        Debug.Log("start host");
    }

    private void NetSettingsOnClientStart(IPAddress ip, ushort port)
    {
        StartClient(ip, port);
    }


}