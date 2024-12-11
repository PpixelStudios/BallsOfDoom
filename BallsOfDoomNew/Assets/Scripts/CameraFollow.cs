using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referência ao jogador
    public Vector3 offset;   // Distância entre a câmera e o jogador
    public float smoothSpeed = 0.125f; // Velocidade do movimento suave

    void LateUpdate()
    {
        // Posição desejada da câmera
        Vector3 desiredPosition = player.position + offset;

        // Interpolação suave para um movimento mais natural
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualizar a posição da câmera
        transform.position = smoothedPosition;

        // Manter a câmera olhando para o jogador (opcional)
        transform.LookAt(player);
    }
}

/*
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // O alvo que a câmara irá seguir (a bola).
    public float distance = 10.0f; // Distância fixa da câmara ao alvo.
    public float height = 5.0f; // Altura da câmara em relação ao alvo.
    public float rotationSpeed = 5.0f; // Velocidade de rotação da câmara ao redor do alvo.

    private Vector3 lastPosition; // Última posição do alvo para calcular a direção.

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("O target não foi atribuído à câmara.");
        }

        // Inicializa a última posição como a posição inicial do alvo.
        lastPosition = target.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a direção do movimento do alvo.
            Vector3 direction = target.position - lastPosition;
            lastPosition = target.position;

            // Verifica se o alvo está em movimento.
            if (direction.magnitude > 0.01f)
            {
                // Calcula o ângulo desejado com base na direção do movimento.
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                // Move a câmara em uma órbita ao redor do alvo.
                Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
                Vector3 positionOffset = rotation * new Vector3(0, height, -distance);
                transform.position = target.position + positionOffset;

                // Faz a câmara olhar para o alvo.
                transform.LookAt(target.position + Vector3.up * (height / 2));
            }
        }
    }
}*/
