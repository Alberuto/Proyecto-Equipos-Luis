using UnityEngine;

public class TeclaPiano : MonoBehaviour {

    public AudioClip sonidoNota;
    private AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    public void Activar() {
        // Sonido
        if (sonidoNota != null && audioSource != null)
            audioSource.PlayOneShot(sonidoNota);

        // Aquí metes anim, feedback, notificación al sistema de secuencias, etc.
        // Ejemplo: TECLA QUE PARPADÉE O HAGA ALGUN EFECTO VISUAL CUANDO SE ACTIVA COMO BAJAR BRILLO , CAMBIAR COLOR, ETC.
        // FindObjectOfType<SequenceManager>().OnNotaPulsada(this);
    }
    // Esto lo usaría tu compañero para la colisión del player
    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {
            Activar();
        }
    }
}