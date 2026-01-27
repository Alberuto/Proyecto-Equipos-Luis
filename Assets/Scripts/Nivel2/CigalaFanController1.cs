using System.Collections;
using UnityEngine;

public class CigalaFanController : MonoBehaviour {

    [Header("Lista de 4 copias del abanico (ya posicionados)")]
    public GameObject[] fanWaves = new GameObject[4];

    [Header("Movimiento relativo")]
    public float moveDistanceX = 10f;
    public float moveDistanceZ = 5f;

    [Header("Configuración")]
    public float totalDuration = 5f;  // Cada uno dura 5 segundos
    public float waveDelay = 0.5f;    // Pausa entre oleadas

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

        Vector3 endPos = initialPositions[index];
        endPos.x += moveDistanceX;
        endPos.z += moveDistanceZ;

        float elapsed = 0f;
        while (elapsed < totalDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / totalDuration;
            fanWaves[index].transform.position = Vector3.Lerp(initialPositions[index], endPos, t);
            yield return null;
        }

        fanWaves[index].SetActive(false);
    }
}