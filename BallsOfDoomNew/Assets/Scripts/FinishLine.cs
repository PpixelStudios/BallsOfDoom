using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool raceFinished = false;
    private int playerCount = 0;
    void OnTriggerEnter(Collider other)
    {
        if (!raceFinished && other.CompareTag("Player"))
        {
            playerCount++;

            if (playerCount == 1)
            { // Primeiro jogador vai para a cena "W".
                GameManager.Instance.LoadNextLevel("W");
                Debug.Log($"{other.name} ganhou a corrida e foi para a cena W!");
            }
            else if (playerCount == 2)
            { // Segundo jogador vai para a cena "L".
                GameManager.Instance.LoadNextLevel("L");
                Debug.Log($"{other.name} chegou em segundo e foi para a cena L!");
                raceFinished = true;
            }
        }
    }


 private int GetBallIndex(string ballName)
    {
        // Assuma que os nomes das bolas são "Ball1", "Ball2", etc.
        if (ballName.StartsWith("Ball"))
        {
            return int.Parse(ballName.Substring(4)) - 1; // Extrai o índice da bola.
        }
        return -1; // Retorna -1 se o nome não corresponde.
    }
}
