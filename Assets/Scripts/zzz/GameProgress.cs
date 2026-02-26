using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgress : MonoBehaviour {
    public static GameProgress Instance { get; private set; }
    public int nivelActual = -1; //nivel 0 desbloqueado, nivel 0 = nivel1 desbloqueado, sucesivamente
    void Awake() {
        if (Instance != null && Instance != this)  {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Cargar nivel desbloqueado
        nivelActual = PlayerPrefs.GetInt("NivelActual", 1);
    }

    public void CompletarNivelActual() {
        nivelActual++;
        PlayerPrefs.SetInt("NivelActual", nivelActual);
        PlayerPrefs.Save();
        Debug.Log($"🎉 Nivel {nivelActual - 1} completado → Desbloqueado Nivel {nivelActual}");
    }
}
