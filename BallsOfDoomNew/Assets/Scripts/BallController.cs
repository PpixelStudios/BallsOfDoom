using Photon.Pun;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f;
    public float sprintMultiplier = 1.5f; // Multiplicador de velocidade para sprint
    public float jumpForce = 5f; // For�a do salto
    public Transform cameraTransform; // Refer�ncia � c�mera
    public KeyCode lookBackKey = KeyCode.LeftShift; // Tecla para olhar para tr�s
    public Animator animator; // Refer�ncia ao Animator

    private Rigidbody rb;
    private bool isLookingBack = false; // Estado da c�mera (normal ou olhando para tr�s)
    private bool isJumping = false; // Estado de pulo
    private bool isSprinting = false; // Estado de sprint
    private bool isBackSprinting = false; // Estado de sprint para tr�s
    private bool isRolling = false; // Estado de roll (rolando)

    private float lastForwardPressTime = 0f; // Tempo do �ltimo pressionamento para frente
    private float lastBackwardPressTime = 0f; // Tempo do �ltimo pressionamento para tr�s
    private float doubleClickThreshold = 0.3f; // Tempo m�ximo entre cliques para detec��o de double-click

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        // Localizar a c�mera automaticamente se n�o estiver atribu�da
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Verificar se o Animator foi atribu�do
        if (animator == null)
        {
            Debug.LogError("Animator n�o atribu�do!");
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

        // Verifica se a tecla para olhar para tr�s est� pressionada
        isLookingBack = Input.GetKey(lookBackKey);

        // Dire��o da c�mera
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

        // Atualizar par�metros no Animator
        UpdateAnimatorParameters(movement);
    }

    void HandleSprint(float moveVertical)
    {
        // Detecta se o movimento vertical est� para frente
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
            isSprinting = false; // Desativa o sprint quando a tecla n�o � mais pressionada
        }

        // Detecta o duplo clique para tr�s (para sprint para tr�s)
        if (moveVertical < 0)
        {
            if (Time.time - lastBackwardPressTime < doubleClickThreshold)
            {
                isBackSprinting = true; // Ativa o sprint para tr�s
            }
            lastBackwardPressTime = Time.time;
        }
        else
        {
            isBackSprinting = false; // Desativa o sprint para tr�s quando a tecla n�o � mais pressionada
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

        // Atualiza o estado do sprint para tr�s
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
        // Verifica se h� colis�o com o ch�o usando um raio abaixo da bola
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    // Adicionado: M�todo para obter posi��o do jogador
    public Vector3 GetPosition()
    {
        return transform.position; // Retorna a posi��o da bola do jogador.
    }

    // Adicionado: M�todo para calcular dist�ncia relativa ao jogador
    public float GetRelativeDistance(Vector3 botPosition)
    {
        Vector3 directionToBot = botPosition - transform.position;
        float forwardDot = Vector3.Dot(directionToBot.normalized, cameraTransform.forward);
        return directionToBot.magnitude * Mathf.Sign(forwardDot);
    }
}
