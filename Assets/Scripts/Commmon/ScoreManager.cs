using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager Instance;

    public string nombreJugador;
    public int scoreTotal = 0;  // ← ACUMULA Nivel0 + Nivel1 + Nivel2
    public int nivelActual = 0;

    private void Awake() {

        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);  // ← PERSISTE SIEMPRE
    }
    private void CargarNombre() {
        nombreJugador = PlayerPrefs.GetString("NombreJugador", "Anónimo");
        Debug.Log($"👤 Nombre cargado: '{nombreJugador}'");
    }
    public void GuardarNombre(string nuevoNombre) {
        nombreJugador = nuevoNombre;
        PlayerPrefs.SetString("NombreJugador", nuevoNombre);
        PlayerPrefs.Save();  // ← INMEDIATO al disco
        Debug.Log($"💾 Nombre guardado: '{nombreJugador}'");
    }
    public void Inicializar() {
        if (nombreJugador == "Anónimo") {  // Solo si no está seteado
            CargarNombre();
        }
    }
}