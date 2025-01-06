using Photon.Pun; // Importa o namespace do Photon
using UnityEngine;

public class MatchmakingManager : MonoBehaviourPunCallbacks // Altera��o aqui: herdar de MonoBehaviourPunCallbacks
{
    public GameObject ballPrefab; // O prefab da bola

    // M�todo para criar a sala
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("Room1", new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
    }

    // M�todo para entrar em uma sala
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("Room1");
    }

    // Este m�todo � chamado quando o jogador entra na sala
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        // Instancia a bola para este jogador ap�s entrar na sala
        PhotonNetwork.Instantiate(ballPrefab.name, Vector3.zero, Quaternion.identity);
    }
}
