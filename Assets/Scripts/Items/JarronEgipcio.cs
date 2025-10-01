using UnityEngine;

public class JarronEgipcio : MonoBehaviour, IInteractuable
{
    [Header("Configuración del Jarrón")]
    public string nombreJarron = "Jarrón Egipcio";
    public TipoJeroglifico jeroglifico;
    
    [Header("Estado")]
    public bool estaColocado = false;
    public BaseJarron baseActual;

    private Rigidbody rb;
    private ObjetoAgarrable agarrable;

    public enum TipoJeroglifico
    {
        Ankh,       // ☥
        Ojo,        // 𓂀
        Escarabajo, // 𓆣
        Piramide,   // 𓉾
        Falcon      // 𓅃
    }

    void Start()
    {
        agarrable = GetComponent<ObjetoAgarrable>();
        if (agarrable == null)
        {
            agarrable = gameObject.AddComponent<ObjetoAgarrable>();
        }
        
        agarrable.nombreObjeto = nombreJarron;
        agarrable.sePuedeAgarrar = !estaColocado;

        rb = GetComponent<Rigidbody>();
    }

    public void Interactuar()
    {
        Debug.Log($"Jarrón con jeroglífico: {jeroglifico}");
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Detectar si entramos en una base
        BaseJarron baseDetectada = other.GetComponent<BaseJarron>();
        
        if (baseDetectada != null && !estaColocado && rb != null )
        {
            IntentarColocarEnBase(baseDetectada);
        }
    }

    void IntentarColocarEnBase(BaseJarron nuevaBase)
    {
        // Verificar si el jeroglífico coincide
        Debug.Log($"Intentando colocar jarrón {nombreJarron} en base {nuevaBase.name}");
        if (nuevaBase.jeroglifico == this.jeroglifico && !nuevaBase.tieneJarron)
        {
            Debug.Log($"Colocando jarrón {nombreJarron} en base {nuevaBase.name}");
            ColocarEnBase(nuevaBase);
        }
        else
        {
            Debug.Log("Este jarrón no pertenece a esta base");
        }
    }

    void ColocarEnBase(BaseJarron nuevaBase)
    {
        estaColocado = true;
        baseActual = nuevaBase;
        nuevaBase.tieneJarron = true;
        nuevaBase.jarronColocado = this;

        // Posicionar correctamente
        transform.position = nuevaBase.puntoColocacion.position;
        transform.rotation = nuevaBase.puntoColocacion.rotation;

        // Desactivar física
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // No se puede volver a agarrar
        if (agarrable != null)
        {
            agarrable.sePuedeAgarrar = false;
        }

        Debug.Log($"¡Jarrón {jeroglifico} colocado correctamente!");
        
        // Notificar al manager del puzzle
        PuzzleManager puzzleManager = FindObjectOfType<PuzzleManager>();
        if (puzzleManager != null)
        {
            puzzleManager.VerificarPuzzleCompletado();
        }
    }
}