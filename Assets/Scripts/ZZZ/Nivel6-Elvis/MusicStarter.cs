using UnityEngine;

public class MusicStarter : MonoBehaviour {
    void Start() {

        var musica = FindObjectOfType<DefenseMusicUniversal>();
        if (musica != null) {
            musica.ReproducirMusica();
        }
    }
}