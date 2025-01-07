using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviourPunCallbacks
{
    public static Server Instance { get; private set; }
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }

    public override void OnJoinedRoom()
    {
        ChangeLevel("Level1");
    }

    internal void CreateRoom(string roomID)
    {
        //Optional
        //Set your room properties if need
        RoomOptions roomOptions = new RoomOptions() {
            MaxPlayers = 5
        };

        PhotonNetwork.CreateRoom(roomID, roomOptions);
    }

    internal void JoinRoom(string roomID)
    {
        PhotonNetwork.JoinRoom(roomID);
    }
    
    internal GameObject InstantiateGameObject(string goName, Vector3 pos, Quaternion rotation)
    {
        return PhotonNetwork.Instantiate(goName, pos, rotation);
    }

    internal void ChangeLevel(string name)
    {
        PhotonNetwork.LoadLevel(name);
    }
}