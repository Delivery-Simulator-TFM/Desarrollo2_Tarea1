using UnityEngine;
using TMPro; // Si usas TextMeshPro

public class LinternaController : MonoBehaviour
{
    [Header("Configuración")]
    public Light luzLinterna;
    public KeyCode teclaLinterna = KeyCode.X;
    public bool linternaEncendida = false;
    
    [Header("UI")]
    public TextMeshProUGUI textoMensaje;
    public float tiempoMensaje = 2f;
    
    [Header("Efectos")]
    public bool efectoParpadeo = false;
    public float intensidadMinima = 1.5f;
    public float intensidadMaxima = 2.5f;
    public float velocidadParpadeo = 5f;

    private float tiempoMostrarMensaje;

    void Start()
    {
        if (luzLinterna == null)
        {
            luzLinterna = GetComponent<Light>();
        }

        if (luzLinterna != null)
        {
            luzLinterna.enabled = linternaEncendida;
        }

        if (textoMensaje != null)
        {
            textoMensaje.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaLinterna))
        {
            ToggleLinterna();
        }

        if (linternaEncendida && efectoParpadeo && luzLinterna != null)
        {
            EfectoParpadeo();
        }

        // Ocultar mensaje después de un tiempo
        if (tiempoMostrarMensaje > 0)
        {
            tiempoMostrarMensaje -= Time.deltaTime;
            if (tiempoMostrarMensaje <= 0 && textoMensaje != null)
            {
                textoMensaje.gameObject.SetActive(false);
            }
        }
    }

    void ToggleLinterna()
    {
        linternaEncendida = !linternaEncendida;

        if (luzLinterna != null)
        {
            luzLinterna.enabled = linternaEncendida;
        }

        MostrarMensaje(linternaEncendida ? "Linterna ENCENDIDA [X]" : "Linterna APAGADA [X]");
    }

    void EfectoParpadeo()
    {
        float parpadeo = Mathf.Lerp(intensidadMinima, intensidadMaxima, 
                                    Mathf.PerlinNoise(Time.time * velocidadParpadeo, 0f));
        luzLinterna.intensity = parpadeo;
    }

    void MostrarMensaje(string mensaje)
    {
        if (textoMensaje != null)
        {
            textoMensaje.text = mensaje;
            textoMensaje.gameObject.SetActive(true);
            tiempoMostrarMensaje = tiempoMensaje;
        }
    }
}