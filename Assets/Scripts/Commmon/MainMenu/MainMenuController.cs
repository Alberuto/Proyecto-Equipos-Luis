using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    private void Awake()    {
        // GUARDAR nivel actual ANTES de borrar
        int nivelActual = PlayerPrefs.GetInt("NivelActual", 0);

        // BORRAR TODO
        PlayerPrefs.DeleteAll();

        // RESTAURAR nivel actual (indestructible)
        PlayerPrefs.SetInt("NivelActual", nivelActual);
        PlayerPrefs.Save();
        Debug.Log($"🆕 MainMenu: Nivel guardado = {nivelActual}");
    }
    public void CargarNivelProgreso() {
        int nivel = PlayerPrefs.GetInt("NivelActual", 0);
        Debug.Log($"🚀 CONTINUAR → Nivel {nivel}");

        switch (nivel) {
            default: SceneManager.LoadScene("Nivel0"); break;
            case 0: SceneManager.LoadScene("Nivel0"); break; // Tutorial
            case 1: SceneManager.LoadScene("Nivel1"); break;
            case 2: SceneManager.LoadScene("Nivel2"); break;
            case 3: SceneManager.LoadScene("Nivel2"); break;
            case 4: SceneManager.LoadScene("Nivel2"); break;
            case 5: SceneManager.LoadScene("Nivel2"); break;
        }
    }
}