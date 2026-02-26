using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SecuenciaManager : MonoBehaviour {

    public string[] secuenciaCorrecta = { "D", "C", "F", "E" };

    private List<string> secuenciaJugador = new List<string>();

    [SerializeField] private LamparaController lampara;

    bool ComprobarSecuencia() {
        return secuenciaJugador.SequenceEqual(secuenciaCorrecta);
    }
    void ReiniciarSecuencia() {
        secuenciaJugador.Clear();
    }
    public void AgregarNota(string nombreNota) {
        secuenciaJugador.Add(nombreNota);
        Debug.Log($"Nota {nombreNota} agregada. Secuencia: {string.Join(", ", secuenciaJugador)}");

        if (secuenciaJugador.Count == secuenciaCorrecta.Length) {

            if (ComprobarSecuencia())  {
                Debug.Log("✅ ¡CORRECTO! → CompletarYAvanzar()");
              //  FindObjectOfType<sceneManager>().CompletarYAvanzar("Nivel");
            }
            else {
                Debug.Log("❌ Incorrecto");
                ReiniciarSecuencia();
            }
        }
    }
}