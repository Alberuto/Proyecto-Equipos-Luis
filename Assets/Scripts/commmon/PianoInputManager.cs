using UnityEngine;
public class PianoInputManager : MonoBehaviour {
    [System.Serializable]
    public class PianoKey {

        public string nombreNota;     // "C", "C#", etc (solo informativo)
        public KeyCode keyCode;       // F1..F12
        public GameObject teclaObjeto; // Cubo de la tecla en escena
    }

    [Header("Teclas del piano (12)")]
    public PianoKey[] teclas;  // Size = 12 en el Inspector

    void Update() {

        for (int i = 0; i < teclas.Length; i++)  {
            PianoKey k = teclas[i];
            if (k == null || k.teclaObjeto == null) continue;
            if (Input.GetKeyDown(k.keyCode)){
                ActivarTecla(k);
            }
        }

        /*
            if (Input.GetKeyDown(KeyCode.F1)) ActivarTecla(teclas[0]);
            if (Input.GetKeyDown(KeyCode.F2)) ActivarTecla(teclas[1]);
            if (Input.GetKeyDown(KeyCode.F3)) ActivarTecla(teclas[2]);
            if (Input.GetKeyDown(KeyCode.F4)) ActivarTecla(teclas[3]);
            if (Input.GetKeyDown(KeyCode.F5)) ActivarTecla(teclas[4]);
            if (Input.GetKeyDown(KeyCode.F6)) ActivarTecla(teclas[5]);
            if (Input.GetKeyDown(KeyCode.F7)) ActivarTecla(teclas[6]);
            if (Input.GetKeyDown(KeyCode.F8)) ActivarTecla(teclas[7]);
            if (Input.GetKeyDown(KeyCode.F9)) ActivarTecla(teclas[8]);
            if (Input.GetKeyDown(KeyCode.F10)) ActivarTecla(teclas[9]);
            if (Input.GetKeyDown(KeyCode.F11)) ActivarTecla(teclas[10]);
            if (Input.GetKeyDown(KeyCode.F12)) ActivarTecla(teclas[11]);  

        if (Input.GetKeyDown(KeyCode.Alpha1)) ActivarTecla(teclas[0]);  // 1 = C
        if (Input.GetKeyDown(KeyCode.Alpha2)) ActivarTecla(teclas[1]);  // 2 = C#
        if (Input.GetKeyDown(KeyCode.Alpha3)) ActivarTecla(teclas[2]);  // 3 = D
        if (Input.GetKeyDown(KeyCode.Alpha4)) ActivarTecla(teclas[3]);  // 4 = D#
        if (Input.GetKeyDown(KeyCode.Alpha5)) ActivarTecla(teclas[4]);  // 5 = E
        if (Input.GetKeyDown(KeyCode.Alpha6)) ActivarTecla(teclas[5]);  // 6 = F
        if (Input.GetKeyDown(KeyCode.Alpha7)) ActivarTecla(teclas[6]);  // 7 = F#
        if (Input.GetKeyDown(KeyCode.Alpha8)) ActivarTecla(teclas[7]);  // 8 = G
        if (Input.GetKeyDown(KeyCode.Alpha9)) ActivarTecla(teclas[8]);  // 9 = G#
        if (Input.GetKeyDown(KeyCode.Alpha0)) ActivarTecla(teclas[9]);  // 0 = A
        if (Input.GetKeyDown(KeyCode.Minus))  ActivarTecla(teclas[10]);  // - = A#
        if (Input.GetKeyDown(KeyCode.Equals)) ActivarTecla(teclas[11]); // + = B */
    }
    void ActivarTecla(PianoKey k) {
        Debug.Log("Tecla " + k.nombreNota + " activada por teclado: " + k.keyCode);

        // Aquí llamas a la misma lógica que usarás en la colisión
        // Por ejemplo, un componente en la tecla:
        TeclaPiano tp = k.teclaObjeto.GetComponent<TeclaPiano>();

        if (tp != null) {

            tp.Activar();   // mismo método que llamará el trigger del jugador
        }
    }
    void Start() {

        Debug.Log("PianoInputManager INICIADO. Teclas configuradas: " + teclas.Length);

        foreach (var k in teclas)
            if (k != null && k.teclaObjeto != null)
                Debug.Log("Tecla OK: " + k.nombreNota);
            else
                Debug.LogError("Tecla MAL configurada!");
    }

}