using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkButtons : NetworkBehaviour
{
    public TextMeshProUGUI m_NumberOfPlayers;

    public NetworkVariable<int> m_PlayersNums = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    // Update is called once per frame
    void Update()
    {
        m_NumberOfPlayers.text = "Players: " + m_PlayersNums.Value.ToString();
        if (!IsServer)
        {
            return;
        }
        m_PlayersNums.Value = NetworkManager.Singleton.ConnectedClients.Count;
        
    }

    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void ServerButton()
    {
        NetworkManager.Singleton.StartServer();
    }
    public void ClientButton()
    {
        NetworkManager.Singleton.StartClient();
    }
}
