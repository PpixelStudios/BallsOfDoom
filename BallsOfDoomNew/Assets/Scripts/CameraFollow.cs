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
