using UnityEngine;

public class MainMenuController : MonoBehaviour {
    private void Awake()    {
        // GUARDAR nivel actual ANTES de borrar
        int nivelActual = PlayerPrefs.GetInt("NivelActual", 1);

        // BORRAR TODO
        PlayerPrefs.DeleteAll();

        // RESTAURAR nivel actual (indestructible)
        PlayerPrefs.SetInt("NivelActual", nivelActual);
        PlayerPrefs.Save();
        Debug.Log($"🆕 MainMenu: Nivel guardado = {nivelActual}");
    }
}