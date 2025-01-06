using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun; // Adicione essa referência para trabalhar com Photon

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int[] scores = new int[4]; // Array para guardar a pontuação de cada bola (0 a 3).
    public GameObject ballPrefab; // Referência ao prefab da bola
    public Transform spawnPoint;  // Ponto onde a bola será instanciada

    private void Awake()
    {
        // Garante que este objeto persiste entre cenas.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Quando a cena for carregada e o jogo começar, instanciar a bola para o jogador
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            InstantiatePlayerBall(); // Instancia a bola com o Photon
        }
    }

    // Método para instanciar a bola do jogador com Photon
    void InstantiatePlayerBall()
    {
        if (ballPrefab != null && spawnPoint != null)
        {
            // Instancia a bola usando Photon
            PhotonNetwork.Instantiate(ballPrefab.name, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab ou ponto de spawn não configurados.");
        }
    }

    // Método para atualizar a pontuação de uma bola.
    public void AddScore(int ballIndex, int points)
    {
        if (ballIndex >= 0 && ballIndex < scores.Length)
        {
            scores[ballIndex] += points;
            Debug.Log($"Bola {ballIndex + 1} agora tem {scores[ballIndex]} pontos.");
        }
    }

    // Método para carregar a próxima cena.
    public void LoadNextLevel(string nextSceneName)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
