using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PersonajeDialogo : MonoBehaviour {

    [Header("UI References")]
    public Image imagenPersonaje;
    public string nombrePersonaje; // "Kiko", "Cigala", "Fary"
    public string estado;

    void Start() {
        CambiarEstado(); //AUTOMÁTICO al cargar escena
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
    public void EmpezarPelea() {
        // Encuentra manager y va a ataque
        FindObjectOfType<Nivel1_Manager>().IrAtaque();
    }
}