using UnityEngine;

public class BallAI : MonoBehaviour
{
    public Transform[] waypoints; // Lista de waypoints que a IA irá seguir.
    public float moveSpeed = 5.0f; // Velocidade de movimento.
    public float rotationSpeed = 5.0f; // Velocidade de rotação.
    public float waypointThreshold = 1.0f; // Distância mínima para considerar que chegou ao waypoint.

    private int currentWaypointIndex = 0; // Índice do waypoint atual.
    private State currentState; // Estado atual da máquina de estados.

    private enum State
    {
        MovingToWaypoint, // Indo para o próximo waypoint.
        AtFinishLine // Chegou à meta.
    }

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints não configurados para a IA.");
            return;
        }

        currentState = State.MovingToWaypoint; // Estado inicial.
    }

    void Update()
    {
        switch (currentState)
        {
            case State.MovingToWaypoint:
                MoveToWaypoint();
                break;
            case State.AtFinishLine:
                Debug.Log("A IA chegou à meta!");
                break;
        }
    }

    private void MoveToWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            // Se todos os waypoints foram alcançados, muda para o estado de chegada.
            currentState = State.AtFinishLine;
            return;
        }

        // Obter o waypoint atual.
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Calcular a direção até o waypoint.
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Mover a bola em direção ao waypoint.
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotacionar suavemente na direção do waypoint.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Verificar se o waypoint foi alcançado.
        if (Vector3.Distance(transform.position, targetWaypoint.position) <= waypointThreshold)
        {
            currentWaypointIndex++; // Avançar para o próximo waypoint.
        }
    }
}
