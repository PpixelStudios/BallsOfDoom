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
