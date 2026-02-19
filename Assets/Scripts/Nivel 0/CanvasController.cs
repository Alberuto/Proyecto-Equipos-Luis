using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour {

    [Header("Canvas Win")] 
    [SerializeField] private GameObject canvasWin;     // Canvas tutorial fallado

    [Header("Canvas Fury")]
    [SerializeField] private GameObject canvasFury;          // Post-tutorial Fury

    [Header("Canvas Lose")]
    [SerializeField] private GameObject canvasLose;          // Canvas tutorial completado

    void Start() {

        // Ocultar todos inicialmente
        OcultarTodosCanvas();

        // Leer PlayerPrefs y activar correcto
        if (PlayerPrefs.GetInt("FuryTutorialCompletado", 0) == 1) {
            ActivarCanvasLose();
        }
        else if (PlayerPrefs.GetInt("FuryTutorialFallado", 0) == 1) {
            ActivarCanvasWin();
        }
        else {
            ActivarCanvasFury();
        }
    }
    private void OcultarTodosCanvas() {
        if (canvasWin) canvasWin.SetActive(false);
        if (canvasFury) canvasFury.SetActive(false);
        if (canvasLose) canvasLose.SetActive(false);
    }
    private void ActivarCanvasWin() {
        if (canvasWin) canvasWin.SetActive(true);
        Debug.Log("📋 Canvas Principal activado");
    }
    private void ActivarCanvasFury() {
        if (canvasFury) canvasFury.SetActive(true);
        PlayerPrefs.DeleteKey("FuryTutorialFallado"); //
        Debug.Log("🔥 Canvas Fury activado - ¡Tutorial completado!");
    }
    private void ActivarCanvasLose() {
        if (canvasLose) canvasLose.SetActive(true);
        PlayerPrefs.DeleteKey("FuryTutorialCompletado"); // Limpia estado win
        Debug.Log("😢 Canvas Lose activado");
    }
    // Botones públicos
    public void IrANivel0b() { // Botón Fury → Secuencias {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel0b");
    }
    public void ReintentarTutorial() {  // Botón Lose → Tutorial otra vez
        PlayerPrefs.DeleteKey("FuryTutorialCompletado");
        PlayerPrefs.DeleteKey("FuryTutorialFallado");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel0");
    }
    public void VolverMenuPrincipal() {
        SceneManager.LoadScene("MainMenu");
    }
}