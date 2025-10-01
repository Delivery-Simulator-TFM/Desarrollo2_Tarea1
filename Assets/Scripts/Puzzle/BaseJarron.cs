using UnityEngine;

public class BaseJarron : MonoBehaviour
{
    [Header("Configuración")]
    public JarronEgipcio.TipoJeroglifico jeroglifico;
    public Transform puntoColocacion; // Punto exacto donde aparece el jarrón
    
    [Header("Estado")]
    public bool tieneJarron = false;
    public JarronEgipcio jarronColocado;

    [Header("Visual (Opcional)")]
    public GameObject simboloJeroglifico; // Modelo 3D o sprite del símbolo

    void Start()
    {
        // Crear punto de colocación si no existe
        if (puntoColocacion == null)
        {
            GameObject punto = new GameObject("PuntoColocacion");
            punto.transform.parent = transform;
            punto.transform.localPosition = Vector3.up * 0.5f; // Ajustar altura
            puntoColocacion = punto.transform;
        }

        // El collider debe ser trigger
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    void OnDrawGizmos()
    {
        // Visualizar la base en el editor
        Gizmos.color = tieneJarron ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 0.1f, 1));
        
        if (puntoColocacion != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(puntoColocacion.position, 0.2f);
        }
    }
}