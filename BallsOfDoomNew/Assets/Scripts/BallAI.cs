using UnityEngine;

public class BallAI : MonoBehaviour
{
    public Transform[] waypoints; // Lista de waypoints que a IA ir� seguir.
    public float baseSpeed = 5.0f; // Velocidade base de movimento.
    public float maxSpeed = 10.0f; // Velocidade m�xima de movimento.
    public float rotationSpeed = 5.0f; // Velocidade de rota��o.
    public float waypointThreshold = 1.0f; // Dist�ncia m�nima para considerar que chegou ao waypoint.
    public float adaptiveThreshold = 20.0f; // Dist�ncia para come�ar o ajuste adaptativo.
    public BallController player; // Refer�ncia � bola do jogador.

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

        if (player == null)
        {
            Debug.LogError("Player (BallController) n�o atribu�do!");
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
