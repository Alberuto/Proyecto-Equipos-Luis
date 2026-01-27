using UnityEngine;
using System.Collections;

public class CigalaFanWaveController1 : MonoBehaviour
{
    [Header("Lista de 4 copias del abanico")]
    public GameObject[] fanWaves = new GameObject[4];  // [0]=sup-izq, [1]=sup-der, [2]=inf-izq, [3]=inf-der

    [Header("Configuración")]
    public float totalDuration = 5f;  // 5 segundos atravesando cubo
    public float waveDelay = 1f;      // Delay entre oleadas

    private Vector3[] initialPositions;
    private int currentWave = 0;

    void Start()
    {
        initialPositions = new Vector3[4];

        for (int i = 0; i < fanWaves.Length; i++)
        {
            if (fanWaves[i] != null)
            {
                initialPositions[i] = fanWaves[i].transform.position;
                fanWaves[i].SetActive(false);
            }
        }

        StartCoroutine(WaveSequence());
    }

    IEnumerator WaveSequence()
    {
        while (true)
        {
            yield return StartCoroutine(LaunchWave(currentWave));
            currentWave = (currentWave + 1) % fanWaves.Length;
            yield return new WaitForSeconds(waveDelay);
        }
    }

    IEnumerator LaunchWave(int index)
    {
        if (fanWaves[index] == null) yield break;

        fanWaves[index].transform.position = initialPositions[index];
        fanWaves[index].SetActive(true);

        Vector3 startPos = initialPositions[index];
        Vector3 endPos = startPos;

        // DIAGONAL AUTOMÁTICA 1:1 X-Z (más rápido)
        float diagonalDistance = 12f;  // Distancia fija diagonal

        if (index == 0 || index == 2)  // Izquierda (sup-izq, inf-izq)
        {
            endPos.x += diagonalDistance;  // Derecha
        }
        else                           // Derecha (sup-der, inf-der)
        {
            endPos.x -= diagonalDistance;  // Izquierda
        }

        if (index == 0 || index == 1)  // Superiores
        {
            endPos.z -= diagonalDistance;  // Hacia atrás
        }
        else                           // Inferiores
        {
            endPos.z += diagonalDistance;  // Hacia adelante
        }

        float elapsed = 0f;
        while (elapsed < totalDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / totalDuration;
            fanWaves[index].transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        fanWaves[index].SetActive(false);
    }
}