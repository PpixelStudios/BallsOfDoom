using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Refer�ncia ao jogador (bola)
    public Vector3 offset = new Vector3(0, 2, -5);   // Dist�ncia entre a c�mera e a bola
    public float smoothSpeed = 0.125f; // Velocidade do movimento suave
    public float rotationSpeed = 100f; // Velocidade de rota��o da c�mera
    public KeyCode lookBackKey = KeyCode.LeftShift; // Tecla para olhar para tr�s

    private bool isLookingBack = false; // Estado da c�mera (normal ou olhando para tr�s)
    private float currentRotationY = 0f; // Armazena a rota��o em Y (horizontal)
    private float currentRotationX = 0f; // Armazena a rota��o em X (vertical), limitada para evitar que a c�mera gire muito para cima ou para baixo

    void LateUpdate()
    {
        // Verifica se a tecla para olhar para tr�s est� pressionada
        isLookingBack = Input.GetKey(lookBackKey);

        // Calcula o offset desejado baseado no estado atual (normal ou olhando para tr�s)
        Vector3 desiredOffset = isLookingBack ? new Vector3(0, 1, 5) : offset; // Ajuste de altura ao olhar para tr�s

        // Permite a rota��o horizontal com o movimento do mouse (ou outra entrada)
        if (!isLookingBack)
        {
            currentRotationY += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime * 2; // Rota��o horizontal
            currentRotationX -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime; // Rota��o vertical

            // Limita a rota��o vertical para evitar que a c�mera vire muito para cima ou para baixo
            currentRotationX = Mathf.Clamp(currentRotationX, -30f, 60f);
        }

        // Calcula a rota��o da c�mera com base na rota��o em Y
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);

        // Calcula a posi��o desejada da c�mera (sempre atr�s da bola, mas com rota��o horizontal)
        Vector3 desiredPosition = player.position + rotation * desiredOffset;

        // Suaviza o movimento da c�mera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualiza a posi��o da c�mera
        transform.position = smoothedPosition;

        // A c�mera sempre olha para a bola
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
}
*/