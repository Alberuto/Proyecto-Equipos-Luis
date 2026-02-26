using UnityEngine;
using UnityEngine.SceneManagement;

public class Nivel1_Manager : MonoBehaviour {
    
    [Header("Paneles de Diálogos")]
    public GameObject panelNormal;
    public GameObject panelFury;
    public GameObject panelWin;
    public GameObject panelLose;

    private bool bossEnFury = false;

    void Start() {

        // Primera vez = Normal, o lee PlayerPrefs para Fury
        if (PlayerPrefs.HasKey("FuryActivado") && PlayerPrefs.GetInt("FuryActivado") == 1) {
            bossEnFury = true;
            ActivarPanel(panelFury);
            PlayerPrefs.DeleteKey("FuryActivado");
        }
        else {
            ActivarPanel(panelNormal);
        }
    }
    void ActivarPanel(GameObject panel) {
        // Desactiva todos
        if (panelNormal != null) panelNormal.SetActive(false);
        if (panelFury != null) panelFury.SetActive(false);
        if (panelWin != null) panelWin.SetActive(false);
        if (panelLose != null) panelLose.SetActive(false);

        // Activa el correcto
        if (panel != null) panel.SetActive(true);
    }
    public void IrAtaque() {
        // DESACTIVA TODO antes de salir
        ActivarPanel(null); // Desactiva todos
        SceneManager.LoadScene("Nivel1-Ataque");
    }
}