using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class viniloController : MonoBehaviour
{
    private float delay = 5f; // Tiempo de espera entre la activación de cada hijo
    private void Start()
    {
        StartCoroutine(activarvinilos());
    }
    private void Update()
    {
        if (GameManager.instance.isFuria())
        {
            delay= 2f; // Reduce el tiempo de espera durante la furia
        }
        if (UIManager.instance.tiempoActual == 100)
        {
            GameManager.instance.ModoFuria(true); // Activa la furia cuando el tiempo llega a 100 Prueba, se puede eliminar después de las pruebas
        }
    }

    IEnumerator activarvinilos()
    {
        foreach (Transform hijo in transform)
        {
            Debug.Log("Hijo encontrado: " + hijo.name);
            hijo.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
    }
}
