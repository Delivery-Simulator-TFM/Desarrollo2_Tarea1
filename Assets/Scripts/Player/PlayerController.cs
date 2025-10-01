using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadCaminar = 5f;
    public float velocidadCorrer = 8f;
    public float fuerzaSalto = 5f;
    public float gravedad = -9.81f;

    [Header("Cámara")]
    public float sensibilidadMouse = 2f;
    public Transform camaraTransform;
    public float anguloMaximo = 90f;

    private CharacterController controller;
    private Vector3 velocidadVertical;
    private float rotacionX = 0f;
    private bool enSuelo;

    void Start()
    {
        // Obtener componentes
        controller = gameObject.AddComponent<CharacterController>();
        controller.height = 2f;
        controller.radius = 0.25f;
        controller.center = new Vector3(0, 0, 0);

        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Si no asignaste la cámara, buscarla
        if (camaraTransform == null)
        {
            camaraTransform = GetComponentInChildren<Camera>().transform;
        }
    }

    void Update()
    {
        Movimiento();
        RotacionCamara();
        
        // Saltar
        if (Input.GetButtonDown("Jump") && enSuelo)
        {
            velocidadVertical.y = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);
        }

        // Desbloquear cursor con ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Movimiento()
    {
        // Verificar si está en el suelo
        enSuelo = controller.isGrounded;

        if (enSuelo && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f;
        }

        // Input de movimiento
        float horizontal = Input.GetAxis("Horizontal"); // A/D
        float vertical = Input.GetAxis("Vertical");     // W/S

        // Dirección de movimiento
        Vector3 movimiento = transform.right * horizontal + transform.forward * vertical;

        // Velocidad (shift para correr)
        float velocidadActual = Input.GetKey(KeyCode.LeftShift) ? velocidadCorrer : velocidadCaminar;

        // Mover
        controller.Move(movimiento * velocidadActual * Time.deltaTime);

        // Aplicar gravedad
        velocidadVertical.y += gravedad * Time.deltaTime;
        controller.Move(velocidadVertical * Time.deltaTime);
    }

    void RotacionCamara()
    {
        // Obtener movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse;

        // Rotar el jugador horizontalmente
        transform.Rotate(Vector3.up * mouseX);

        // Rotar la cámara verticalmente
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -anguloMaximo, anguloMaximo);
        camaraTransform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
    }
}