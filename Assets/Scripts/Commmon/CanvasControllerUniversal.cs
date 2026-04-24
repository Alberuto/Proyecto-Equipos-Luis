using System.Text.RegularExpressions;
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
        Debug.Log($"Antes de cargar Nivel5: Nivel={PlayerPrefs.GetInt("Nivel", 0)} Fallo={PlayerPrefs.GetInt("Fallo", 0)} Fury={PlayerPrefs.GetInt("Fury", 0)} VidaBoss={PlayerPrefs.GetFloat("VidaBoss", 100f)}");
        int numeroEscena = ObtenerNumeroEscena();

        if (numeroEscena == 2 || numeroEscena == 4 || numeroEscena == 6) {
            var musica = FindObjectOfType<DefenseMusicUniversalNPC>();
            if (musica != null) {
                musica.ReproducirMusica();
            }
        }
    }
    void GestionarEstadoCanvas() {
        // 🎯 La carga de escenas esta gestionada por sceneManager los player prefs se gestionan en cada attack manager particular

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
            PlayerPrefs.SetFloat("VidaJugador", 100f);
            PlayerPrefs.Save();
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
    private int ObtenerNumeroEscena() {
        string nombreEscena = SceneManager.GetActiveScene().name;
        Match match = Regex.Match(nombreEscena, @"\d+");
        Debug.Log("numero escena NPC" + int.Parse(match.Value));
        return match.Success ? int.Parse(match.Value) : -1;
    }
}