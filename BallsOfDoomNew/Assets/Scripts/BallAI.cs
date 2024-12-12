using UnityEngine;

public class BallAI : MonoBehaviour
{
    public Transform[] waypoints; // Lista de waypoints que a IA ir� seguir.
    public float moveSpeed = 5.0f; // Velocidade de movimento.
    public float rotationSpeed = 5.0f; // Velocidade de rota��o.
    public float waypointThreshold = 1.0f; // Dist�ncia m�nima para considerar que chegou ao waypoint.

    private int currentWaypointIndex = 0; // �ndice do waypoint atual.
    private State currentState; // Estado atual da m�quina de estados.

    private enum State
    {
        MovingToWaypoint, // Indo para o pr�ximo waypoint.
        AtFinishLine // Chegou � meta.
    }

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints n�o configurados para a IA.");
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
                Debug.Log("A IA chegou � meta!");
                break;
        }
    }

    private void MoveToWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            // Se todos os waypoints foram alcan�ados, muda para o estado de chegada.
            currentState = State.AtFinishLine;
            return;
        }

        // Obter o waypoint atual.
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Calcular a dire��o at� o waypoint.
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Mover a bola em dire��o ao waypoint.
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotacionar suavemente na dire��o do waypoint.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Verificar se o waypoint foi alcan�ado.
        if (Vector3.Distance(transform.position, targetWaypoint.position) <= waypointThreshold)
        {
            currentWaypointIndex++; // Avan�ar para o pr�ximo waypoint.
        }
    }
}
