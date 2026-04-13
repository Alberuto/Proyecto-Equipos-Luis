using System.Collections;
using UnityEngine;

public class DefenseMusicUniversalNPC : MonoBehaviour {

    [Header("Trozos Musicales NPC")]
    public AudioClip[] Trozos;
    private AudioSource audioSource;
    private int turnoNPC = 0;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        turnoNPC = PlayerPrefs.GetInt("TurnoNPC", 0);
    }
    public void ReproducirMusica() {

        if (Trozos == null || Trozos.Length == 0 || audioSource == null) 
            return;

        int indice = turnoNPC % Trozos.Length;
        AudioClip clip = Trozos[indice];

        if (clip == null) 
            return;

        audioSource.clip = clip;
        audioSource.Play();
        Debug.Log($"🎵 NPC Trozo {indice}");
        StopAllCoroutines();
        StartCoroutine(CambiarAlSiguienteCuandoTermine(clip));
    }
    private IEnumerator CambiarAlSiguienteCuandoTermine(AudioClip clip)  {
        double duracion = (double)clip.samples / clip.frequency;
        yield return new WaitForSeconds((float)duracion);
        turnoNPC++;
        PlayerPrefs.SetInt("TurnoNPC", turnoNPC);
        PlayerPrefs.Save();
        ReproducirMusica();
    }
}