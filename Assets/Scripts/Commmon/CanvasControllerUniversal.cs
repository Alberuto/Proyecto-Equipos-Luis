using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasControllerUniversal : MonoBehaviour
{

    [Header("Canvas Estados")]
    [SerializeField] private GameObject canvasNormal;   // Primera vez
    [SerializeField] private GameObject canvasWin;      // IA gana  
    [SerializeField] private GameObject canvasFury;     // Fase 1 OK
    [SerializeField] private GameObject canvasLose;     // Nivel completo

    void Start() {

        OcultarTodosCanvas();
        GestionarEstadoCanvas();
    }

    void GestionarEstadoCanvas()
    {
        // 🎯 PlayerPrefs gestionados por sceneManager

        if (PlayerPrefs.GetInt($"Fallo", 0) == 1) {
            ActivarCanvasWin();
        }
        else if (PlayerPrefs.GetInt($"Nivel", 0) == 1) {
            ActivarCanvasLose();
        }
        else if (PlayerPrefs.GetInt($"Fury", 0) == 1) {
            ActivarCanvasFury();
        }
        else {
            ActivarCanvasNormal();
        }
    }
    private void OcultarTodosCanvas() {

        if (canvasNormal) canvasNormal.SetActive(false);
        if (canvasWin) canvasWin.SetActive(false);
        if (canvasFury) canvasFury.SetActive(false);
        if (canvasLose) canvasLose.SetActive(false);
    }
    private void ActivarCanvasNormal() { if (canvasNormal) canvasNormal.SetActive(true); }
    private void ActivarCanvasWin() { if (canvasWin) canvasWin.SetActive(true); }
    private void ActivarCanvasFury() { if (canvasFury) canvasFury.SetActive(true); }
    private void ActivarCanvasLose() { if (canvasLose) canvasLose.SetActive(true); }

}