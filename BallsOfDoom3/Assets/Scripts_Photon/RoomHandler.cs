using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomHandler : MonoBehaviourPunCallbacks
{
    public void CreateRoom()
    {
        string roomName = "Room_" + Random.Range(1000, 9999);
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 });
        Debug.Log($"Tentando criar sala: {roomName}");
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Tentando entrar em uma sala aleatória...");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Entrou na sala: {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarning("Falha ao entrar em uma sala aleatória. Criando uma nova...");
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Falha ao criar a sala: {message}");
    }
}
