using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Configuración")]
    public BaseJarron[] bases; // Las 5 bases
    public GameObject ataud; // El ataúd que se abre
    public GameObject breakerPrefab; // El prefab del breaker
    public Transform puntoApareceBreaker;

    [Header("Animación")]
    public float velocidadApertura = 1f;
    private bool puzzleCompletado = false;

    [Header("Audio")]
    public AudioClip sonidoPuzzleCompletado;
    public AudioClip sonidoJarronColocado;
    public AudioClip ataudAbierto;
    private AudioSource audioSource;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Start()
    {
        // Encontrar todas las bases si no están asignadas
        if (bases == null || bases.Length == 0)
        {
            bases = FindObjectsOfType<BaseJarron>();
        }

        // Suscribirse al evento de cada jarrón
        JarronEgipcio[] jarrones = FindObjectsOfType<JarronEgipcio>();
        foreach (JarronEgipcio jarron in jarrones)
        {
            jarron.onJarronColocado += OnJarronColocado;
        }
    }

    void OnJarronColocado()
    {
        if (sonidoJarronColocado != null)
        {
            audioSource.PlayOneShot(sonidoJarronColocado);
        }
        VerificarPuzzleCompletado();
    }

    public void VerificarPuzzleCompletado()
    {
        if (puzzleCompletado) return;

        // Verificar si todas las bases tienen su jarrón
        foreach (BaseJarron baseJarron in bases)
        {
            if (!baseJarron.tieneJarron)
            {
                Debug.Log("Faltan jarrones por colocar");
                return;
            }
        }

        // ¡Puzzle completado!
        CompletarPuzzle();
    }

    void CompletarPuzzle()
    {
        puzzleCompletado = true;
        Debug.Log("¡PUZZLE COMPLETADO! Abriendo ataúd...");


        // Abrir ataúd
        if (ataud != null)
        {
            StartCoroutine(AbrirAtaud());
        }

        // Instanciar breaker
        if (breakerPrefab != null && puntoApareceBreaker != null)
        {
            GameObject breaker = Instantiate(breakerPrefab, puntoApareceBreaker.position, puntoApareceBreaker.rotation);

            // Agregar componente agarrable al breaker si no lo tiene
            ObjetoAgarrable agarrable = breaker.GetComponent<ObjetoAgarrable>();
            if (agarrable == null)
            {
                agarrable = breaker.AddComponent<ObjetoAgarrable>();
                agarrable.nombreObjeto = "Breaker";
            }
        }
        audioSource.PlayOneShot(sonidoPuzzleCompletado);
    }

    System.Collections.IEnumerator AbrirAtaud()
    {
        Debug.Log("Abriendo ataúd...");
        // Aquí puedes agregar la lógica para abrir el ataúd
        Vector3 posicionInicial = ataud.transform.position;
        Vector3 posicionFinal = posicionInicial + Vector3.right * 1.5f; // Sube 1.5 metros
        audioSource.PlayOneShot(ataudAbierto);

        float tiempo = 0;
        while (tiempo < 1f)
        {
            tiempo += Time.deltaTime * velocidadApertura;
            ataud.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempo);
            yield return null;
        }

        // Detener el sonido después de 1 segundo
        yield return new WaitForSeconds(1f);
        audioSource.Stop();
    }
}