using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referência ao jogador (bola)
    public Vector3 offset = new Vector3(0, 2, -5);   // Distância entre a câmera e a bola
    public float smoothSpeed = 0.125f; // Velocidade do movimento suave
    public float rotationSpeed = 100f; // Velocidade de rotação da câmera
    public KeyCode lookBackKey = KeyCode.LeftShift; // Tecla para olhar para trás

    private bool isLookingBack = false; // Estado da câmera (normal ou olhando para trás)
    private float currentRotationY = 0f; // Armazena a rotação em Y (horizontal)
    private float currentRotationX = 0f; // Armazena a rotação em X (vertical), limitada para evitar que a câmera gire muito para cima ou para baixo

    void LateUpdate()
    {
        // Verifica se a tecla para olhar para trás está pressionada
        isLookingBack = Input.GetKey(lookBackKey);

        // Calcula o offset desejado baseado no estado atual (normal ou olhando para trás)
        Vector3 desiredOffset = isLookingBack ? new Vector3(0, 1, 5) : offset; // Ajuste de altura ao olhar para trás

        // Permite a rotação horizontal com o movimento do mouse (ou outra entrada)
        if (!isLookingBack)
        {
            currentRotationY += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime * 2; // Rotação horizontal
            currentRotationX -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime; // Rotação vertical

            // Limita a rotação vertical para evitar que a câmera vire muito para cima ou para baixo
            currentRotationX = Mathf.Clamp(currentRotationX, -30f, 60f);
        }

        // Calcula a rotação da câmera com base na rotação em Y
        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);

        // Calcula a posição desejada da câmera (sempre atrás da bola, mas com rotação horizontal)
        Vector3 desiredPosition = player.position + rotation * desiredOffset;

        // Suaviza o movimento da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualiza a posição da câmera
        transform.position = smoothedPosition;

        // A câmera sempre olha para a bola
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
}
*/