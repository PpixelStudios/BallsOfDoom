using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f;
    public Transform cameraTransform; // Refer�ncia � c�mera
    public KeyCode lookBackKey = KeyCode.LeftShift; // Tecla para olhar para tr�s

    private Rigidbody rb;
    private bool isLookingBack = false; // Estado da c�mera (normal ou olhando para tr�s)

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Localizar a c�mera automaticamente se n�o estiver atribu�da
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

        // Verifica se a tecla para olhar para tr�s est� pressionada
        isLookingBack = Input.GetKey(lookBackKey);

        // Dire��o da c�mera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Se estamos olhando para tr�s, invertemos apenas o movimento visual da c�mera, n�o a dire��o real
        if (isLookingBack)
        {
            forward = -forward; // Inverte apenas a dire��o do movimento visual
        }

        // Calcular movimento
        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        rb.AddForce(movement * speed);
    }
}
