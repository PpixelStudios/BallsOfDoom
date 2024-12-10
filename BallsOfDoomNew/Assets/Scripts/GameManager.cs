using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int[] scores = new int[4]; // Array para guardar a pontuação de cada bola (0 a 3).

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
