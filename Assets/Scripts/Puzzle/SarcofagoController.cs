using UnityEngine;

public class SarcofagoController : MonoBehaviour
{
    [SerializeField]
    private GameObject tapaSarcofago;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tapaSarcofago == null)
        {
            Debug.LogWarning("tapaSarcofago no está asignado en el inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tapaSarcofago != null)
        {
            tapaSarcofago.transform.Rotate(0, 20 * Time.deltaTime, 0);
        }
    }
}
