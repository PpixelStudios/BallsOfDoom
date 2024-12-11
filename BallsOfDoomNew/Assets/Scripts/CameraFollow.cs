using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Refer�ncia ao jogador
    public Vector3 offset;   // Dist�ncia entre a c�mera e o jogador
    public float smoothSpeed = 0.125f; // Velocidade do movimento suave

    void LateUpdate()
    {
        // Posi��o desejada da c�mera
        Vector3 desiredPosition = player.position + offset;

        // Interpola��o suave para um movimento mais natural
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualizar a posi��o da c�mera
        transform.position = smoothedPosition;

        // Manter a c�mera olhando para o jogador (opcional)
        transform.LookAt(player);
    }
}

/*
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target; // O alvo que a c�mara ir� seguir (a bola).
    public float distance = 10.0f; // Dist�ncia fixa da c�mara ao alvo.
    public float height = 5.0f; // Altura da c�mara em rela��o ao alvo.
    public float rotationSpeed = 5.0f; // Velocidade de rota��o da c�mara ao redor do alvo.

    private Vector3 lastPosition; // �ltima posi��o do alvo para calcular a dire��o.

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("O target n�o foi atribu�do � c�mara.");
        }

        // Inicializa a �ltima posi��o como a posi��o inicial do alvo.
        lastPosition = target.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a dire��o do movimento do alvo.
            Vector3 direction = target.position - lastPosition;
            lastPosition = target.position;

            // Verifica se o alvo est� em movimento.
            if (direction.magnitude > 0.01f)
            {
                // Calcula o �ngulo desejado com base na dire��o do movimento.
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                // Move a c�mara em uma �rbita ao redor do alvo.
                Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
                Vector3 positionOffset = rotation * new Vector3(0, height, -distance);
                transform.position = target.position + positionOffset;

                // Faz a c�mara olhar para o alvo.
                transform.LookAt(target.position + Vector3.up * (height / 2));
            }
        }
    }
}*/
