using UnityEngine;
using System.Collections;

public class BallAI : MonoBehaviour
{
    public Transform[] waypoints; // Lista de waypoints que a IA seguir�.
    public float moveSpeed = 5.0f; // Velocidade de movimento.
    public float rotationSpeed = 5.0f; // Velocidade de rota��o.
    public float waypointThreshold = 1.0f; // Dist�ncia m�nima para considerar que chegou ao waypoint.
    public float obstacleDetectionRange = 3.0f; // Alcance para detectar obst�culos (raycast central).
    public float sideDetectionRange = 5.0f; // Alcance para os raycasts laterais (maior que o central).
    public LayerMask obstacleLayer; // Camada para identificar obst�culos.
    public float sideStepDistance = 3.0f; // Dist�ncia para desviar para o lado.
    public float checkInterval = 0.2f; // Intervalo entre verifica��es de obst�culos no estado de desvio.
    public float sideStepTime = 1.0f; // Tempo para mover lateralmente antes de verificar o caminho.
    public float sideRaycastAngle = 45f; // O �ngulo dos raycasts laterais em rela��o ao raycast central.

    private int currentWaypointIndex = 0; // �ndice do waypoint atual.
    private State currentState; // Estado atual da IA.
    private float lastCheckTime; // Tempo da �ltima verifica��o de obst�culos no estado de desvio.
    private Quaternion initialRotation; // A rota��o inicial da bola.

    private enum State
    {
        MovingToWaypoint, // Seguindo os waypoints.
        AvoidingObstacle // Desviando de um obst�culo.
    }

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints n�o configurados para a IA.");
            return;
        }

        currentState = State.MovingToWaypoint; // Estado inicial.
        initialRotation = transform.rotation; // Salvar a rota��o inicial.
    }

    void Update()
    {
        switch (currentState)
        {
            case State.MovingToWaypoint:
                MoveToWaypoint();
                break;
            case State.AvoidingObstacle:
                AvoidObstacle();
                break;
        }
    }

    private void MoveToWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            Debug.Log("A IA alcan�ou o �ltimo waypoint.");
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Detectar obst�culos ao redor.
        if (IsObstacleDetected())
        {
            Debug.Log("Obst�culo detectado! Mudando para o estado AvoidingObstacle.");
            currentState = State.AvoidingObstacle;
            return;
        }

        // Mover na dire��o do waypoint.
        MoveInDirection(direction);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= waypointThreshold)
        {
            currentWaypointIndex++;
        }
    }

    private void AvoidObstacle()
    {
        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;

            if (!IsObstacleDetected())
            {
                Debug.Log("Caminho livre! Voltando para o estado MovingToWaypoint.");
                currentState = State.MovingToWaypoint;
                return;
            }

            // Tentar mover lateralmente para a dire��o que est� mais livre.
            StartCoroutine(MoveSidewaysUntilClear());
        }
    }

    private bool IsObstacleDetected()
    {
        // Raycast central para detectar obst�culos � frente da bola.
        Vector3 direction = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, obstacleDetectionRange, obstacleLayer))
        {
            // Desenho do raycast central para depura��o.
            Debug.DrawRay(transform.position, direction * obstacleDetectionRange, Color.red);
            return true;
        }

        return false;
    }

    private IEnumerator MoveSidewaysUntilClear()
    {
        // Raycast lateral para detectar obst�culos � direita e � esquerda.
        Vector3 rightDirection = Quaternion.Euler(0, sideRaycastAngle, 0) * transform.forward;
        Vector3 leftDirection = Quaternion.Euler(0, -sideRaycastAngle, 0) * transform.forward;

        bool moveRight = !IsObstacleDetectedInDirection(rightDirection);
        bool moveLeft = !IsObstacleDetectedInDirection(leftDirection);

        if (moveRight && !moveLeft)
        {
            // Se o caminho � direita estiver livre, mover para a direita.
            StartCoroutine(MoveAndCheck(rightDirection));
        }
        else if (moveLeft && !moveRight)
        {
            // Se o caminho � esquerda estiver livre, mover para a esquerda.
            StartCoroutine(MoveAndCheck(leftDirection));
        }
        else
        {
            // Se ambos os lados estiverem livres, escolher o lado mais pr�ximo.
            StartCoroutine(MoveAndCheck(moveRight ? rightDirection : leftDirection));
        }

        yield return null;
    }

    private IEnumerator MoveAndCheck(Vector3 direction)
    {
        // Mover a bola na dire��o escolhida por um tempo.
        float startTime = Time.time;

        while (Time.time - startTime < sideStepTime)
        {
            MoveInDirection(direction);
            yield return null;
        }

        // Verificar se o caminho est� livre ap�s o movimento lateral.
        if (!IsObstacleDetected())
        {
            Debug.Log("Caminho livre ap�s o desvio lateral. Voltando � rota��o inicial.");
            transform.rotation = initialRotation;
            currentState = State.MovingToWaypoint;
        }
        else
        {
            // Se ainda houver obst�culo, tentar novamente.
            Debug.Log("Ainda h� um obst�culo. Tentando novamente.");
            currentState = State.AvoidingObstacle;
        }
    }

    private bool IsObstacleDetectedInDirection(Vector3 direction)
    {
        // Raycast para verificar se h� um obst�culo em uma dire��o espec�fica (direita ou esquerda).
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, sideDetectionRange, obstacleLayer))
        {
            // Desenho dos raycasts laterais para depura��o.
            Debug.DrawRay(transform.position, direction * sideDetectionRange, Color.green);
            return true;
        }

        return false;
    }

    private void MoveInDirection(Vector3 direction)
    {
        // Mover a bola na dire��o especificada.
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;

        // Atualizar a rota��o da bola para continuar indo na dire��o especificada.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
