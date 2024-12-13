using UnityEngine;
using System.Collections;

public class BallAI : MonoBehaviour
{
    public Transform[] waypoints; // Lista de waypoints que a IA seguirá.
    public float moveSpeed = 5.0f; // Velocidade de movimento.
    public float rotationSpeed = 5.0f; // Velocidade de rotação.
    public float waypointThreshold = 1.0f; // Distância mínima para considerar que chegou ao waypoint.
    public float obstacleDetectionRange = 3.0f; // Alcance para detectar obstáculos (raycast central).
    public float sideDetectionRange = 5.0f; // Alcance para os raycasts laterais (maior que o central).
    public LayerMask obstacleLayer; // Camada para identificar obstáculos.
    public float sideStepDistance = 3.0f; // Distância para desviar para o lado.
    public float checkInterval = 0.2f; // Intervalo entre verificações de obstáculos no estado de desvio.
    public float sideStepTime = 1.0f; // Tempo para mover lateralmente antes de verificar o caminho.
    public float sideRaycastAngle = 45f; // O ângulo dos raycasts laterais em relação ao raycast central.

    private int currentWaypointIndex = 0; // Índice do waypoint atual.
    private State currentState; // Estado atual da IA.
    private float lastCheckTime; // Tempo da última verificação de obstáculos no estado de desvio.
    private Quaternion initialRotation; // A rotação inicial da bola.

    private enum State
    {
        MovingToWaypoint, // Seguindo os waypoints.
        AvoidingObstacle // Desviando de um obstáculo.
    }

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("Waypoints não configurados para a IA.");
            return;
        }

        currentState = State.MovingToWaypoint; // Estado inicial.
        initialRotation = transform.rotation; // Salvar a rotação inicial.
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
            Debug.Log("A IA alcançou o último waypoint.");
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Detectar obstáculos ao redor.
        if (IsObstacleDetected())
        {
            Debug.Log("Obstáculo detectado! Mudando para o estado AvoidingObstacle.");
            currentState = State.AvoidingObstacle;
            return;
        }

        // Mover na direção do waypoint.
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

            // Tentar mover lateralmente para a direção que está mais livre.
            StartCoroutine(MoveSidewaysUntilClear());
        }
    }

    private bool IsObstacleDetected()
    {
        // Raycast central para detectar obstáculos à frente da bola.
        Vector3 direction = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, obstacleDetectionRange, obstacleLayer))
        {
            // Desenho do raycast central para depuração.
            Debug.DrawRay(transform.position, direction * obstacleDetectionRange, Color.red);
            return true;
        }

        return false;
    }

    private IEnumerator MoveSidewaysUntilClear()
    {
        // Raycast lateral para detectar obstáculos à direita e à esquerda.
        Vector3 rightDirection = Quaternion.Euler(0, sideRaycastAngle, 0) * transform.forward;
        Vector3 leftDirection = Quaternion.Euler(0, -sideRaycastAngle, 0) * transform.forward;

        bool moveRight = !IsObstacleDetectedInDirection(rightDirection);
        bool moveLeft = !IsObstacleDetectedInDirection(leftDirection);

        if (moveRight && !moveLeft)
        {
            // Se o caminho à direita estiver livre, mover para a direita.
            StartCoroutine(MoveAndCheck(rightDirection));
        }
        else if (moveLeft && !moveRight)
        {
            // Se o caminho à esquerda estiver livre, mover para a esquerda.
            StartCoroutine(MoveAndCheck(leftDirection));
        }
        else
        {
            // Se ambos os lados estiverem livres, escolher o lado mais próximo.
            StartCoroutine(MoveAndCheck(moveRight ? rightDirection : leftDirection));
        }

        yield return null;
    }

    private IEnumerator MoveAndCheck(Vector3 direction)
    {
        // Mover a bola na direção escolhida por um tempo.
        float startTime = Time.time;

        while (Time.time - startTime < sideStepTime)
        {
            MoveInDirection(direction);
            yield return null;
        }

        // Verificar se o caminho está livre após o movimento lateral.
        if (!IsObstacleDetected())
        {
            Debug.Log("Caminho livre após o desvio lateral. Voltando à rotação inicial.");
            transform.rotation = initialRotation;
            currentState = State.MovingToWaypoint;
        }
        else
        {
            // Se ainda houver obstáculo, tentar novamente.
            Debug.Log("Ainda há um obstáculo. Tentando novamente.");
            currentState = State.AvoidingObstacle;
        }
    }

    private bool IsObstacleDetectedInDirection(Vector3 direction)
    {
        // Raycast para verificar se há um obstáculo em uma direção específica (direita ou esquerda).
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, sideDetectionRange, obstacleLayer))
        {
            // Desenho dos raycasts laterais para depuração.
            Debug.DrawRay(transform.position, direction * sideDetectionRange, Color.green);
            return true;
        }

        return false;
    }

    private void MoveInDirection(Vector3 direction)
    {
        // Mover a bola na direção especificada.
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;

        // Atualizar a rotação da bola para continuar indo na direção especificada.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
