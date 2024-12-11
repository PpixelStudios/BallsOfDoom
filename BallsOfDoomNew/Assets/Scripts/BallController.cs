using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f;
    public Transform cameraTransform; // Referência à câmera
    public KeyCode lookBackKey = KeyCode.LeftShift; // Tecla para olhar para trás

    private Rigidbody rb;
    private bool isLookingBack = false; // Estado da câmera (normal ou olhando para trás)

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Localizar a câmera automaticamente se não estiver atribuída
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Centralizar o cursor na tela
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(null, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
    }

    void FixedUpdate()
    {
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

        // Se estamos olhando para trás, invertemos apenas o movimento visual da câmera, não a direção real
        if (isLookingBack)
        {
            forward = -forward; // Inverte apenas a direção do movimento visual
        }

        // Calcular movimento
        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        rb.AddForce(movement * speed);
    }
}
