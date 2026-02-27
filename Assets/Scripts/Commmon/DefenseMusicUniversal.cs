using UnityEngine;

public class DefenseMusicUniversal : MonoBehaviour {

    [Header("Trozos Musicales")]
    public AudioClip[] Trozos;

    private AudioSource audioSource;
    private int turnoDefensa = 0;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        turnoDefensa = PlayerPrefs.GetInt("TurnoDefensa", 0);
    }
    public void ReproducirMusica() {

        int indice = turnoDefensa % Trozos.Length;
        if (Trozos[indice] != null) {
            audioSource.clip = Trozos[indice];
            audioSource.Play();
            Debug.Log($"🎵 Trozo {indice}");
        }
    }
    public void SiguienteTurno() {
        turnoDefensa++;
        PlayerPrefs.SetInt("TurnoDefensa", turnoDefensa);
        PlayerPrefs.Save();
    }
}