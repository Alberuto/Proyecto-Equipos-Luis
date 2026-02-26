using UnityEngine;
using System.Collections.Generic;

public class SecuenciaManager : MonoBehaviour {

    public int[] secuenciaCorrecta = { 2, 1, 4, 3 }; // Ejemplo
    private List<int> secuenciaJugador = new List<int>();
    [SerializeField] private LamparaController lampara;

    public void AgregarANotaIluminada(int numeroNota) {

        secuenciaJugador.Add(numeroNota);
        Debug.Log($"📝 Secuencia jugador: {string.Join(", ", secuenciaJugador)}");

        // Comprobar si correcto
        if (secuenciaJugador.Count == secuenciaCorrecta.Length) {

            if (ComprobarSecuencia()) {
                Debug.Log("✅ ¡SECUENCIA CORRECTA! Nivel avanzado");
                lampara.gameObject.SetActive(false); // Quitar lámpara
            }
            else {
                Debug.Log("❌ Secuencia incorrecta. Reiniciar");
                ReiniciarSecuencia();
            }
        }
    }
    bool ComprobarSecuencia() {
        for (int i = 0; i < secuenciaCorrecta.Length; i++) {
            if (secuenciaJugador[i] != secuenciaCorrecta[i]) return false;
        }
        return true;
    }
    void ReiniciarSecuencia() {
        secuenciaJugador.Clear();
    }
}