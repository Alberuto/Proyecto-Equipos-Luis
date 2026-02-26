using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {
    public void CargarNivel(string nombreEscena) {

        SceneManager.LoadScene(nombreEscena);
    }
    public void CompletarYAvanzar(string nombreEscena) {

        // 🆕 Extraer número del string "Nivel1", "Nivel2"...
        int siguienteNivel = ExtraerNumeroDeEscena(nombreEscena);
        // Guardar directamente el nivel desbloqueado
        PlayerPrefs.SetInt("NivelActual", siguienteNivel);
        PlayerPrefs.Save();
        Debug.Log($"🎉 Nivel {siguienteNivel - 1} completado → Desbloqueado hasta Nivel {siguienteNivel}");
        SceneManager.LoadScene(nombreEscena);
    }
    private int ExtraerNumeroDeEscena(string escena) {
        // "Nivel1"→1, "Nivel2"→2, "Nivel10"→10
        Match match = Regex.Match(escena, @"\d+");
        return match.Success ? int.Parse(match.Value) : 0;
    }
    public void CerrarAplicacion() {
        Application.Quit();
    }
}