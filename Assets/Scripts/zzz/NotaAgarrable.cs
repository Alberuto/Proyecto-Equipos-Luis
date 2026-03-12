using UnityEngine;

public class NotaAgarrable : MonoBehaviour {

    public string nombreNota;
    public AudioClip sonidoNota;
    private AudioSource audioSource;

    void Awake() {

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
    public void Activar() {
        if (sonidoNota != null && audioSource != null)
            audioSource.PlayOneShot(sonidoNota);
    }
}