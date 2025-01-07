using Photon.Pun;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f;
    public float sprintMultiplier = 1.5f; // Multiplicador de velocidade para sprint
    public float jumpForce = 5f; // Força do salto
    public Transform cameraTransform; // Referência à câmera
    public KeyCode lookBackKey = KeyCode.LeftShift; // Tecla para olhar para trás
    public Animator animator; // Referência ao Animator

    private Rigidbody rb;
    private bool isLookingBack = false; // Estado da câmera (normal ou olhando para trás)
    private bool isJumping = false; // Estado de pulo
    private bool isSprinting = false; // Estado de sprint
    private bool isBackSprinting = false; // Estado de sprint para trás
    private bool isRolling = false; // Estado de roll (rolando)

    private float lastForwardPressTime = 0f; // Tempo do último pressionamento para frente
    private float lastBackwardPressTime = 0f; // Tempo do último pressionamento para trás
    private float doubleClickThreshold = 0.3f; // Tempo máximo entre cliques para detecção de double-click

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        // Localizar a câmera automaticamente se não estiver atribuída
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Verificar se o Animator foi atribuído
        if (animator == null)
        {
            Debug.LogError("Animator não atribuído!");
        }

        // Centralizar o cursor na tela
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(null, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Verifica se a tecla para olhar para trás está pressionada
        isLookingBack = Input.GetKey(lookBackKey);

        // Direção da câmera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Calcular movimento
        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        // Sprint e Sprint Back
        HandleSprint(moveVertical);

        // Ajustar velocidade com base no sprint
        float currentSpeed = isSprinting ? speed * sprintMultiplier : speed;
        currentSpeed = isBackSprinting ? speed * sprintMultiplier : currentSpeed;

        rb.AddForce(movement * currentSpeed);

        // Salto
        HandleJump();

        // Atualizar parâmetros no Animator
        UpdateAnimatorParameters(movement);
    }

    void HandleSprint(float moveVertical)
    {
        // Detecta se o movimento vertical está para frente
        if (moveVertical > 0)
        {
            // Detecta o duplo clique para ativar o sprint
            if (Time.time - lastForwardPressTime < doubleClickThreshold)
            {
                isSprinting = true; // Ativa o sprint
            }
            lastForwardPressTime = Time.time;
        }
        else
        {
            isSprinting = false; // Desativa o sprint quando a tecla não é mais pressionada
        }

        // Detecta o duplo clique para trás (para sprint para trás)
        if (moveVertical < 0)
        {
            if (Time.time - lastBackwardPressTime < doubleClickThreshold)
            {
                isBackSprinting = true; // Ativa o sprint para trás
            }
            lastBackwardPressTime = Time.time;
        }
        else
        {
            isBackSprinting = false; // Desativa o sprint para trás quando a tecla não é mais pressionada
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    void UpdateAnimatorParameters(Vector3 movement)
    {
        float velocity = rb.velocity.magnitude;

        // Atualiza a velocidade no Animator
        animator.SetFloat("Speed", velocity);

        // Atualiza os estados no Animator
        animator.SetBool("IsSprinting", isSprinting);
        animator.SetBool("IsLookingBack", isLookingBack);
        animator.SetBool("IsJumping", isJumping);

        // Atualiza o estado do sprint para trás
        animator.SetBool("IsBackSprinting", isBackSprinting);

        // Atualiza o estado do roll (rolando)
        if (isRolling)
        {
            animator.SetBool("IsRolling", true);
        }
        else
        {
            animator.SetBool("IsRolling", false);
        }
    }

    bool IsGrounded()
    {
        // Verifica se há colisão com o chão usando um raio abaixo da bola
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    // Adicionado: Método para obter posição do jogador
    public Vector3 GetPosition()
    {
        return transform.position; // Retorna a posição da bola do jogador.
    }

    // Adicionado: Método para calcular distância relativa ao jogador
    public float GetRelativeDistance(Vector3 botPosition)
    {
        Vector3 directionToBot = botPosition - transform.position;
        float forwardDot = Vector3.Dot(directionToBot.normalized, cameraTransform.forward);
        return directionToBot.magnitude * Mathf.Sign(forwardDot);
    }
}
