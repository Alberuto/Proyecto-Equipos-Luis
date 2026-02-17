using UnityEngine;
using System.Collections.Generic;

public class AttackManager : MonoBehaviour
{
    [Header("Secuencia objetivo")]
    private string secuenciaHeredada = "";
    private List<string> secuenciaObjetivo = new List<string>();
    private List<string> secuenciaJugador = new List<string>();
    private bool ataqueActivo = false;

    /*[Header("Mapeo de notas")]
    public string[] notasOrden = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" }; Aluncinacion IA*/

    [Header("Feedback")]
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoError;
    private AudioSource feedbackAudio;

    void Start() {
        Debug.Log("?? AttackManager INICIADO");
        Debug.Log("?? AudioSource: " + (GetComponent<AudioSource>() != null));
        feedbackAudio = GetComponent<AudioSource>();
    }
    // Método auxiliar para convertir string de attack selector en List<string> para comparación
    private List<string> ConvertirSecuenciaStringALista(string secuencia) {

        List<string> listaNotas = new List<string>();
        string notaActual = "";

        for (int i = 0; i < secuencia.Length; i++) {

            char c = secuencia[i];

            if (c == '#') {
                notaActual += c;
            }
            else {
                // Si hay nota anterior, la guardamos
                if (!string.IsNullOrEmpty(notaActual)) {

                    listaNotas.Add(notaActual);
                }
                notaActual = c.ToString();
            }
        }
        // Guardar última nota
        if (!string.IsNullOrEmpty(notaActual)) {

            listaNotas.Add(notaActual);
        }
        return listaNotas;
    }
    // Llamado desde AttackSelector
    public void IniciarAtaque(string secuencia) {



        secuenciaObjetivo = ConvertirSecuenciaStringALista(secuencia);  //AQUÍ
        secuenciaHeredada = secuencia;
        secuenciaJugador.Clear();
        ataqueActivo = true;
        Debug.Log("¡Ataque iniciado! Objetivo: [" + string.Join(", ", secuenciaObjetivo) + "]");
    }
    public void RegistrarNota(string nota) {

        if (!ataqueActivo) return;

        secuenciaJugador.Add(nota);
        Debug.Log("Nota: " + nota + " | Progreso: [" + string.Join(", ", secuenciaJugador) + "]");

        if (feedbackAudio != null && sonidoCorrecto != null)
            feedbackAudio.PlayOneShot(sonidoCorrecto);

        // Comparación lista vs lista
        bool coincideHastaAhora = true;
        for (int i = 0; i < secuenciaJugador.Count; i++) {

            if (secuenciaJugador[i] != secuenciaObjetivo[i]) {

                coincideHastaAhora = false;
                break;
            }
        }
        if (!coincideHastaAhora) {
            FalloAtaque();
            return;
        }
        if (secuenciaJugador.Count == secuenciaObjetivo.Count) {
            ExitoAtaque();
        }
    }
    void ExitoAtaque() {
        ataqueActivo = false;
        Debug.Log("¡ATAQUE EXITOSO! Secuencia: " + string.Join("", secuenciaJugador));
    }
    void FalloAtaque() {
        ataqueActivo = false;
        Debug.Log("¡ATAQUE FALLIDO!");
        if (feedbackAudio != null && sonidoError != null)
            feedbackAudio.PlayOneShot(sonidoError);
    }
}