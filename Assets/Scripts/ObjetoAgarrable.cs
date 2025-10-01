using UnityEngine;

public class ObjetoAgarrable : MonoBehaviour, IInteractuable
{
    [Header("Configuración")]
    public string nombreObjeto = "Objeto";
    public bool sePuedeAgarrar = true;
    
    [Header("Física")]
    public float masa = 1f;

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
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void Interactuar()
    {
        Debug.Log($"Interactuando con: {nombreObjeto}");
    }
}