using System;
using UnityEngine;

public class BaseJarron : MonoBehaviour
{
    [Header("Configuración")]
    public JarronEgipcio.TipoJeroglifico jeroglifico;
    public Transform puntoColocacion;
    
    [Header("Estado")]
    public bool tieneJarron = false;
    public JarronEgipcio jarronColocado;

    [Header("Visual (Opcional)")]
    public GameObject simboloJeroglifico;

    public Action onJarronColocado;

    void Start()
    {
        // Crear punto de colocación si no existe
        if (puntoColocacion == null)
        {
            GameObject punto = new GameObject("PuntoColocacion");
            punto.transform.parent = transform;
            punto.transform.localPosition = Vector3.up * 0.5f;
            puntoColocacion = punto.transform;
        }

        // Asegurar que el collider sea trigger
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
        else
        {
            Debug.LogWarning($"Base {gameObject.name} no tiene Collider!");
        }
    }

    void OnDrawGizmos()
    {
        // Visualizar en el editor
        Gizmos.color = tieneJarron ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 0.1f, 1));
        
        if (puntoColocacion != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(puntoColocacion.position, 0.2f);
        }
    }
}