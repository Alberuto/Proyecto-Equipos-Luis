using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour {

    [Header("Canvas Normal")]
    [SerializeField] private GameObject canvasNormal; // primera vez

    [Header("Canvas Win")]
    [SerializeField] private GameObject canvasWin;     // Dodeca gana (tutorial fallado)

    [Header("Canvas Fury")]
    [SerializeField] private GameObject canvasFury;    // segunda parte tutorial

    [Header("Canvas Lose")]
    [SerializeField] private GameObject canvasLose;    // Tutorial completado (Fury WIN)
    void Start() {
        OcultarTodosCanvas();
        // 🎯 LÓGICA PRIMERA EJECUCIÓN
        if (!PlayerPrefs.HasKey("JuegoIniciado")) {

            // PRIMERA VEZ → Canvas normal
            PlayerPrefs.SetInt("JuegoIniciado", 1);
            PlayerPrefs.Save();
            ActivarCanvasNormal();
            Debug.Log("🌟 PRIMERA EJECUCIÓN - Canvas Fury (normal)");
        }
        else if (PlayerPrefs.GetInt("FuryTutorialCompletado", 0) == 1) //1 parte
        {
            ActivarCanvasFury();  // Fury WIN
        }
        else if (PlayerPrefs.GetInt("FuryTutorialFallado", 0) == 1) {
            ActivarCanvasWin();   // Dodeca gana
        }
        else if (PlayerPrefs.GetInt("Nivel0bCompletado", 0) ==1) {
            ActivarCanvasLose();  // Normal (ya jugado) // tutorial completado
        }
    }
    private void OcultarTodosCanvas() {
        if (canvasNormal) canvasNormal.SetActive(false);
        if (canvasWin) canvasWin.SetActive(false);
        if (canvasFury) canvasFury.SetActive(false);
        if (canvasLose) canvasLose.SetActive(false);
    }

    private void ActivarCanvasWin() {
        if (canvasWin) canvasWin.SetActive(true);
        Debug.Log("📋 Canvas Win Dodecafonismo activado");
    }
    private void ActivarCanvasFury() {
        if (canvasFury) canvasFury.SetActive(true);
        PlayerPrefs.DeleteKey("FuryTutorialFallado");
        Debug.Log("🔥 Canvas Fury (normal) activado");
    }
    private void ActivarCanvasNormal() {
        if (canvasNormal) canvasNormal.SetActive(true);
        PlayerPrefs.DeleteKey("FuryTutorialFallado");
        Debug.Log("📋 Canvas NORMAL activado"); 
    }
    private void ActivarCanvasLose() {
        if (canvasLose) canvasLose.SetActive(true);
        PlayerPrefs.DeleteKey("FuryTutorialCompletado");
        Debug.Log("✅ Canvas Lose (Fury WIN - Tutorial completado)");
    }
    // Botones
    public void IrANivel0b()  // Fury → Segunda parte
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel0b");
    }

    public void IrANivel1()   // Completa todo → Nivel 1
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel1");
    }

    public void ReintentarTutorial() {
        PlayerPrefs.DeleteKey("FuryTutorialCompletado");
        PlayerPrefs.DeleteKey("FuryTutorialFallado");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nivel0a");
    }

    public void MenuPrincipal()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
