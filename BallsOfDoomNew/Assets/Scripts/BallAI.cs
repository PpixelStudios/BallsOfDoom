using UnityEngine;

public class BallAI : MonoBehaviour
{
    public Transform finishLine; // Transform da linha de chegada.
    public float moveSpeed = 5.0f; // Velocidade de movimento.
    public float rotationSpeed = 2.0f; // Velocidade de rota��o.
    public float obstacleDetectionRange = 5.0f; // Dist�ncia para detectar obst�culos.

    private State currentState; // Estado atual da m�quina de estados.

    private enum State
    {
        MovingToFinish, // Indo em dire��o � meta.
        AvoidingObstacle, // Desviando de um obst�culo.
        Stuck // Parado ou recuperando-se.
    }

    private void Start()
    {
        currentState = State.MovingToFinish; // Come�a indo para a meta.
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
        // Dire��o para a linha de chegada.
        Vector3 direction = (finishLine.position - transform.position).normalized;

        // Detec��o de obst�culos.
        if (Physics.Raycast(transform.position, transform.forward, obstacleDetectionRange))
        {
            Debug.Log("Obst�culo detectado! Mudando para o estado AvoidingObstacle.");
            currentState = State.AvoidingObstacle;
            return;
        }

        // Movimenta-se na dire��o da linha de chegada.
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotaciona suavemente para olhar para a meta.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void AvoidObstacle()
    {
        // Gira para evitar o obst�culo.
        transform.Rotate(0, rotationSpeed * 50 * Time.deltaTime, 0);

        // Se n�o houver mais obst�culos, volta ao estado MovingToFinish.
        if (!Physics.Raycast(transform.position, transform.forward, obstacleDetectionRange))
        {
            Debug.Log("Obst�culo evitado! Voltando para o estado MovingToFinish.");
            currentState = State.MovingToFinish;
        }
    }

    private void Recover()
    {
        // Recupera a IA ap�s estar presa (pode implementar uma l�gica espec�fica aqui).
        Debug.Log("Recuperando...");
        currentState = State.MovingToFinish; // Volta para o estado de movimenta��o.
    }
}
