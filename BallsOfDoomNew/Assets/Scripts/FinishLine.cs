using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool raceFinished = false;

    void OnTriggerEnter(Collider other)
    {
        if (!raceFinished && other.CompareTag("Player"))
        {
            raceFinished = true;
            // Determina o �ndice da bola a partir do nome do objeto.
            //int ballIndex = GetBallIndex(other.gameObject.name);

            // Atualiza a pontua��o da bola vencedora.
            //GameManager.Instance.AddScore(ballIndex, 10);

            // Transi��o para a pr�xima sala (nome da pr�xima cena).
            GameManager.Instance.LoadNextLevel("W");

            Debug.Log($"{other.name} ganhou a corrida!");
        }
    }

    private int GetBallIndex(string ballName)
    {
        // Assuma que os nomes das bolas s�o "Ball1", "Ball2", etc.
        if (ballName.StartsWith("Ball"))
        {
            return int.Parse(ballName.Substring(4)) - 1; // Extrai o �ndice da bola.
        }
        return -1; // Retorna -1 se o nome n�o corresponde.
    }
}
