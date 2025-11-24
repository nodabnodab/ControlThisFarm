using System.Collections;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject dropPrefab; // ∂≥æÓ∂ﬂ∏± «¡∏Æ∆’
    public float dropInterval = 5f; // «¡∏Æ∆’¿ª ∂≥æÓ∂ﬂ∏± ¡÷±‚ (√  ¥‹¿ß)

    void Start()
    {
        StartCoroutine(DropRoutine());
    }

    IEnumerator DropRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(dropInterval);
            DropPrefab();
        }
    }

    void DropPrefab()
    {
        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
            Debug.Log($"Dropped {dropPrefab.name} at {transform.position}");
        }
        else
        {
            Debug.LogWarning("Drop prefab is not assigned.");
        }
    }
}