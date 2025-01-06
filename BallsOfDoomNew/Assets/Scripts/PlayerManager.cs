using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public GameObject ballPrefab; // O prefab da bola a ser instanciado para cada jogador
    public Transform spawnPoint;  // Ponto de spawn da bola para cada jogador

    void Start()
    {
        // Verifica se o jogador é o mestre (host) ou um jogador normal
        if (photonView.IsMine)
        {
            // Instancia a bola para este jogador
            Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
