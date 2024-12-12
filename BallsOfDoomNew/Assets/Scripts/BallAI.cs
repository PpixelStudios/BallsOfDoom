using UnityEngine;

public class BallAI : MonoBehaviour
{
    public Transform finishLine; // Transform da linha de chegada.
    public float moveSpeed = 5.0f; // Velocidade de movimento.
    public float rotationSpeed = 2.0f; // Velocidade de rotação.
    public float obstacleDetectionRange = 5.0f; // Distância para detectar obstáculos.

    private State currentState; // Estado atual da máquina de estados.

    private enum State
    {
        MovingToFinish, // Indo em direção à meta.
        AvoidingObstacle, // Desviando de um obstáculo.
        Stuck // Parado ou recuperando-se.
    }

    private void Start()
    {
        currentState = State.MovingToFinish; // Começa indo para a meta.
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.MovingToFinish:
                MoveToFinish();
                break;
            case State.AvoidingObstacle:
                AvoidObstacle();
                break;
            case State.Stuck:
                Recover();
                break;
        }
    }

    private void MoveToFinish()
    {
        // Direção para a linha de chegada.
        Vector3 direction = (finishLine.position - transform.position).normalized;

        // Detecção de obstáculos.
        if (Physics.Raycast(transform.position, transform.forward, obstacleDetectionRange))
        {
            Debug.Log("Obstáculo detectado! Mudando para o estado AvoidingObstacle.");
            currentState = State.AvoidingObstacle;
            return;
        }

        // Movimenta-se na direção da linha de chegada.
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotaciona suavemente para olhar para a meta.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void AvoidObstacle()
    {
        // Gira para evitar o obstáculo.
        transform.Rotate(0, rotationSpeed * 50 * Time.deltaTime, 0);

        // Se não houver mais obstáculos, volta ao estado MovingToFinish.
        if (!Physics.Raycast(transform.position, transform.forward, obstacleDetectionRange))
        {
            Debug.Log("Obstáculo evitado! Voltando para o estado MovingToFinish.");
            currentState = State.MovingToFinish;
        }
    }

    private void Recover()
    {
        // Recupera a IA após estar presa (pode implementar uma lógica específica aqui).
        Debug.Log("Recuperando...");
        currentState = State.MovingToFinish; // Volta para o estado de movimentação.
    }
}
