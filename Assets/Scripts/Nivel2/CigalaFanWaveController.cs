using UnityEngine;
using System.Collections;

public class CigalaFanWaveController : MonoBehaviour
{
    [Header("Lista de 4 copias del abanico")]
    public GameObject[] fanWaves = new GameObject[4];

    [Header("Configuración")]
    public float totalDuration = 8f;   // MAS LARGO: 8 segundos
    public float diagonalSpeed = 18f;  // MAS RAPIDO: 18 unidades
    public float waveDelay = 1.2f;     // Delay entre oleadas

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

        // DIAGONAL 1:1 X-Z configurable
        if (index == 0 || index == 2)
            endPos.x += diagonalSpeed;
        else
            endPos.x -= diagonalSpeed;

        if (index == 0 || index == 1)
            endPos.z -= diagonalSpeed;
        else
            endPos.z += diagonalSpeed;

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
