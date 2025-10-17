using UnityEngine;

public class ObjetoAgarrable : MonoBehaviour, IInteractuable
{
    [Header("Configuración")]
    public string nombreObjeto = "Objeto";
    public bool sePuedeAgarrar = true;
    public Transform puntoAgarre;

    [Header("Física")]
    public float masa = 1f;
    public bool usarGravedad = true;


    private Rigidbody rb;

    void Start()
    {
        // Configurar Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.mass = masa;
        rb.useGravity = usarGravedad;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void Interactuar()
    {
        Debug.Log($"Interactuando con: {nombreObjeto}");
    }
}