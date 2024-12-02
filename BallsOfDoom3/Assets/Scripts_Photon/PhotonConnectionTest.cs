using UnityEngine;
using Photon.Pun;

public class PhotonConnectionTest : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Conectar ao servidor Photon
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Conectando ao Photon...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado ao servidor Photon com sucesso!");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Entrou no lobby do Photon.");
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.LogError($"Desconectado do Photon: {cause}");
    }
}
