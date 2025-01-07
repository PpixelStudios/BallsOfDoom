using Photon.Pun;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float spawnOffsetX;

    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            Spawn();
        }
        else Debug.LogWarning("Not in room");
    }

    internal void Spawn()
    {
        Vector3 pos = transform.position + Vector3.right * Random.Range(-spawnOffsetX, spawnOffsetX);

        Transform plyer = Server.Instance.InstantiateGameObject(playerPrefab.name, pos, Quaternion.identity).transform;

        Camera.main.GetComponent<CameraFollow>().player = plyer;
    }
}