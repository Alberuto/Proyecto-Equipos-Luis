using UnityEngine;
using System.Collections;

public class CigalaCardinalWaveController : MonoBehaviour
{
    [Header("Lista de 4 copias cardinales (0:Norte,1:Sur,2:Este,3:Oeste)")]
    public GameObject[] cardinalWaves = new GameObject[4];

    [Header("Configuración independiente")]
    public float totalDuration = 8f;
    public float cardinalSpeed = 18f;
    public float waveDelay = 1.2f;

    private Vector3[] initialPositions;
    private int currentWave = 0;

    void Start()
    {
        initialPositions = new Vector3[4];

        for (int i = 0; i < cardinalWaves.Length; i++)
        {
            if (cardinalWaves[i] != null)
            {
                initialPositions[i] = cardinalWaves[i].transform.position;
                cardinalWaves[i].SetActive(false);
            }
        }

        StartCoroutine(WaveSequence());
    }

    IEnumerator WaveSequence()
    {
        while (true)
        {
            yield return StartCoroutine(LaunchWave(currentWave));
            currentWave = (currentWave + 1) % cardinalWaves.Length;
            yield return new WaitForSeconds(waveDelay);
        }
    }

    IEnumerator LaunchWave(int index)
    {
        if (cardinalWaves[index] == null) yield break;

        cardinalWaves[index].transform.position = initialPositions[index];
        cardinalWaves[index].SetActive(true);

        Vector3 startPos = initialPositions[index];
        Vector3 endPos = startPos;

        // Solo movimiento en un eje
        int cardIndex = index;
        if (cardIndex == 0) // Norte: +Z
            endPos.z += cardinalSpeed;
        else if (cardIndex == 1) // Sur: -Z
            endPos.z -= cardinalSpeed;
        else if (cardIndex == 2) // Este: +X
            endPos.x += cardinalSpeed;
        else // Oeste: -X
            endPos.x -= cardinalSpeed;

        float elapsed = 0f;
        while (elapsed < totalDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / totalDuration;
            cardinalWaves[index].transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        cardinalWaves[index].SetActive(false);
    }
}
