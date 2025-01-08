using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool raceFinished = false;

    void OnTriggerEnter(Collider other)
    {
        if (!raceFinished && other.CompareTag("Player"))
        {
            raceFinished = true;
            // Determina o índice da bola a partir do nome do objeto.
            //int ballIndex = GetBallIndex(other.gameObject.name);

            // Atualiza a pontuação da bola vencedora.
            //GameManager.Instance.AddScore(ballIndex, 10);

            // Transição para a próxima sala (nome da próxima cena).
            GameManager.Instance.LoadNextLevel("W");

            Debug.Log($"{other.name} ganhou a corrida!");
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
