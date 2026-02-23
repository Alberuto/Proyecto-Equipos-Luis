using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeDialogo : MonoBehaviour {

    [Header("UI References")]
    public Image imagenPersonaje;
    public TextMeshProUGUI textoDialogo;

    [Header("Config Personaje")]
    public string nombrePersonaje; // "Kiko", "Cigala", "Fary"
    public string estado;

    [Header("Lineas de diálogo")]
    [TextArea(2, 4)]
    public string[] lineas;        // 1,2,3,4... en Inspector
    int indice = 0;

    void Start() {

        CambiarEstado(); //AUTOMÁTICO al cargar escena
        ActualizarTexto(); // Mostrar primera línea
    }
    public void CambiarEstado() {

        // Ruta: "img/Personaje1/Normal"
        string ruta = $"img/Personajes/{nombrePersonaje}/{estado}";
        // Carga Sprite desde Resources
        Sprite sprite = Resources.Load<Sprite>(ruta);

        if (sprite != null) {
            imagenPersonaje.sprite = sprite;
            Debug.Log($"Cargado {ruta}");
        }
        else {
            Debug.LogError($"No encontrado: {ruta}");
        }
    }
    void ActualizarTexto() {

        if (lineas == null || lineas.Length == 0) return;

        indice = Mathf.Clamp(indice, 0, lineas.Length - 1);
        textoDialogo.text = lineas[indice];
    }

    // Llamar desde botón "Siguiente"
    public void SiguienteTexto() {

        if (lineas == null || lineas.Length == 0) return;

        if (indice < lineas.Length - 1) {
            indice++;
            ActualizarTexto();
        }
        else {
            // Última línea , empezar pelea (solo en panel Normal / Fury, por ejemplo)
            EmpezarPelea();
        }
    }
    // Llamar desde botón "Anterior" (opcional)
    public void AnteriorTexto() {

        if (lineas == null || lineas.Length == 0) return;

        if (indice > 0) {
            indice--;
            ActualizarTexto();
        }
    }
    public void EmpezarPelea() {
        // Encuentra manager y va a ataque
        FindObjectOfType<Nivel1_Manager>().IrAtaque();
    }
}