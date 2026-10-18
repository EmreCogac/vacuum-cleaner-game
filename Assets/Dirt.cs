using UnityEngine;

public class Dirt : MonoBehaviour
{
    private DirtManager dirtManager;

    void Start()
    {
        dirtManager = FindObjectOfType<DirtManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vacuum")) // Süpürgenizin tag'ý "Vacuum" olmalý
        {
            dirtManager.CleanDirt(gameObject);
        }
    }
}