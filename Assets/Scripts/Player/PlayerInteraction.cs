using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuración")]
    public Camera camaraJugador;
    public float distanciaInteraccion = 3f;
    public KeyCode teclaInteraccion = KeyCode.E;
    public LayerMask capasInteractuables;

    [Header("UI")]
    public TextMeshProUGUI textoInteraccion;

    [Header("Transporte de Objetos")]
    public Transform puntoAgarre; // Donde se sostiene el objeto
    public float distanciaAgarre = 2f;
    public float suavidadMovimiento = 10f;

    private GameObject objetoActual; // Objeto que estás mirando
    private GameObject objetoAgarrado; // Objeto que estás cargando
    private Rigidbody rbObjetoAgarrado;

    void Start()
    {
        // Obtener cámara si no está asignada
        if (camaraJugador == null)
        {
            camaraJugador = GetComponentInChildren<Camera>();
        }

        // Crear punto de agarre si no existe
        if (puntoAgarre == null)
        {
            GameObject punto = new GameObject("PuntoAgarre");
            punto.transform.parent = camaraJugador.transform;
            punto.transform.localPosition = new Vector3(0, -0.5f, distanciaAgarre);
            puntoAgarre = punto.transform;
        }

        // Ocultar texto al inicio
        if (textoInteraccion != null)
        {
            textoInteraccion.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Si ya tienes un objeto agarrado, no detectar otros
        if (objetoAgarrado != null)
        {
            MoverObjetoAgarrado();

            // Soltar objeto
            if (Input.GetKeyDown(teclaInteraccion))
            {
                SoltarObjeto();
            }
            return;
        }

        // Detectar objetos interactuables
        DetectarObjeto();

        // Interactuar con objeto
        if (Input.GetKeyDown(teclaInteraccion) && objetoActual != null)
        {
            InteractuarConObjeto();
        }
    }

    void DetectarObjeto()
    {
        Ray ray = camaraJugador.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaInteraccion, capasInteractuables))
        {
            // Hay un objeto interactuable
            GameObject objetoDetectado = hit.collider.gameObject;

            if (objetoDetectado != objetoActual)
            {
                objetoActual = objetoDetectado;
                MostrarMensajeInteraccion(objetoActual);
            }
        }
        else
        {
            // No hay objeto
            if (objetoActual != null)
            {
                objetoActual = null;
                OcultarMensajeInteraccion();
            }
        }
    }

    void InteractuarConObjeto()
    {
        // Obtener componente interactuable
        IInteractuable interactuable = objetoActual.GetComponent<IInteractuable>();
        
        if (interactuable != null)
        {
            interactuable.Interactuar();
        }

        // Si es un objeto que se puede agarrar
        ObjetoAgarrable agarrable = objetoActual.GetComponent<ObjetoAgarrable>();
        
        if (agarrable != null && agarrable.sePuedeAgarrar)
        {
            AgarrarObjeto(objetoActual);
        }
    }

    void AgarrarObjeto(GameObject objeto)
    {
        objetoAgarrado = objeto;
        rbObjetoAgarrado = objeto.GetComponent<Rigidbody>();

        if (rbObjetoAgarrado != null)
        {
            rbObjetoAgarrado.useGravity = false;
            rbObjetoAgarrado.linearDamping = 10f;
            rbObjetoAgarrado.freezeRotation = true;
        }

        OcultarMensajeInteraccion();
        MostrarMensaje("Presiona [E] para soltar");
    }

    void MoverObjetoAgarrado()
    {
        if (objetoAgarrado != null && rbObjetoAgarrado != null)
        {
            Vector3 direccion = puntoAgarre.position - objetoAgarrado.transform.position;
            rbObjetoAgarrado.linearVelocity = direccion * suavidadMovimiento;
        }
    }

    void SoltarObjeto()
    {
        if (rbObjetoAgarrado != null)
        {
            rbObjetoAgarrado.useGravity = true;
            rbObjetoAgarrado.linearDamping = 0f;
        }

        objetoAgarrado = null;
        rbObjetoAgarrado = null;
        OcultarMensajeInteraccion();
    }

    void MostrarMensajeInteraccion(GameObject objeto)
    {
        if (textoInteraccion != null)
        {
            string mensaje = $"Presiona [E] para interactuar con {objeto.name}";
            
            // Personalizar mensaje según tipo de objeto
            ObjetoAgarrable agarrable = objeto.GetComponent<ObjetoAgarrable>();
            if (agarrable != null)
            {
                mensaje = $"Presiona [E] para recoger {agarrable.nombreObjeto}";
            }

            textoInteraccion.text = mensaje;
            textoInteraccion.gameObject.SetActive(true);
        }
    }

    void MostrarMensaje(string mensaje)
    {
        if (textoInteraccion != null)
        {
            textoInteraccion.text = mensaje;
            textoInteraccion.gameObject.SetActive(true);
        }
    }

    void OcultarMensajeInteraccion()
    {
        if (textoInteraccion != null)
        {
            textoInteraccion.gameObject.SetActive(false);
        }
    }

    // Debug: Visualizar el rayo en el editor
    void OnDrawGizmos()
    {
        if (camaraJugador != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 origen = camaraJugador.transform.position;
            Vector3 direccion = camaraJugador.transform.forward * distanciaInteraccion;
            Gizmos.DrawRay(origen, direccion);
        }
    }
}

