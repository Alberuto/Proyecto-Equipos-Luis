using UnityEngine;

public class KikoDefenseMusic : MonoBehaviour {

    [Header("Trozos Musicales")]
    public AudioClip[] Trozos;

    private AudioSource audioSource;
    private int turnoDefensaKiko = 0;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        turnoDefensaKiko = PlayerPrefs.GetInt("Nivel1TurnoDefensaKiko", 0);
    }
    public void ReproducirMusicaKiko() {

        int indice = turnoDefensaKiko % Trozos.Length;
        if (Trozos[indice] != null) {
            audioSource.clip = Trozos[indice];
            audioSource.Play();
            Debug.Log($"🎵 Kiko Trozo {indice}");
        }
    }
    public void SiguienteTurnoKiko(){
        turnoDefensaKiko++;
        PlayerPrefs.SetInt("Nivel1TurnoDefensaKiko", turnoDefensaKiko);
        PlayerPrefs.Save();
    }
}