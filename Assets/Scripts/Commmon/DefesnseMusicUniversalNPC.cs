using UnityEngine;

public class DefenseMusicUniversalNPC : MonoBehaviour {

    [Header("Trozos Musicales NPC")]
    public AudioClip[] Trozos;
    private AudioSource audioSource;
    private int turnoNPC = 0;
    private float tiempoTrozo = 14f;  // Duración de cada trozo
    private float tiempoRestanteTrozo;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        turnoNPC = PlayerPrefs.GetInt("TurnoNPC", 0);
        tiempoRestanteTrozo = tiempoTrozo;
    }
    public void ReproducirMusica() {
        int indice = turnoNPC % Trozos.Length;
        if (Trozos[indice] != null) {
            audioSource.clip = Trozos[indice];
            audioSource.Play();
            Debug.Log($"🎵 NPC Trozo {indice}");
            tiempoRestanteTrozo = tiempoTrozo;
        }
    }
    void Update() {
        if (audioSource.isPlaying && tiempoRestanteTrozo > 0)    {
            tiempoRestanteTrozo -= Time.deltaTime;
            if (tiempoRestanteTrozo <= 0)  {
                SiguienteTrozo();
            }
        }
    }
    void SiguienteTrozo() {
        turnoNPC++;
        PlayerPrefs.SetInt("TurnoNPC", turnoNPC);
        PlayerPrefs.Save();
        ReproducirMusica();  // Siguiente trozo
    }
    public void SiguienteTurno() {
        turnoNPC++;
        PlayerPrefs.SetInt("TurnoNPC", turnoNPC);
        PlayerPrefs.Save();
    }
}