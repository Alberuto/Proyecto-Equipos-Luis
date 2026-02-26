using UnityEngine;

public class TeclaPianoTutorial : MonoBehaviour { 

    [SerializeField] private string nombreNota;
    [SerializeField] private AudioClip sonidoNota;

    private AudioSource audioSource;
    private AttackManagerTutorial attackManager;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        attackManager = FindObjectOfType<AttackManagerTutorial>();
    }
    public void Activar() {
        // Sonido de la tecla (opcional, independiente de OK/Error)
        if (sonidoNota != null && audioSource != null)
            audioSource.PlayOneShot(sonidoNota);

        if (attackManager != null) 
            attackManager.RegistrarNotaJugador(nombreNota);

    }
}