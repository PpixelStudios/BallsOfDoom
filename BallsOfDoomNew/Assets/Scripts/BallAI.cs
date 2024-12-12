using UnityEngine;

public class BallAI : MonoBehaviour
{
    public Transform[] waypoints; // Lista de waypoints que a IA irá seguir.
    public float baseSpeed = 5.0f; // Velocidade base de movimento.
    public float maxSpeed = 10.0f; // Velocidade máxima de movimento.
    public float rotationSpeed = 5.0f; // Velocidade de rotação.
    public float waypointThreshold = 1.0f; // Distância mínima para considerar que chegou ao waypoint.
    public float adaptiveThreshold = 20.0f; // Distância para começar o ajuste adaptativo.
    public BallController player; // Referência à bola do jogador.

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

        if (player == null)
        {
            Debug.LogError("Player (BallController) não atribuído!");
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
            currentState = State.AtFinishLine;
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        float currentSpeed = baseSpeed;

        if (player != null)
        {
            float relativeDistance = player.GetRelativeDistance(transform.position);

            if (relativeDistance < -adaptiveThreshold)
            {
                currentSpeed = Mathf.Lerp(baseSpeed, maxSpeed, -relativeDistance / adaptiveThreshold);
            }
            else if (relativeDistance > adaptiveThreshold)
            {
                currentSpeed = Mathf.Lerp(baseSpeed, 0, relativeDistance / adaptiveThreshold);
            }
        }

        transform.position += direction * currentSpeed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= waypointThreshold)
        {
            currentWaypointIndex++;
        }
    }
}
