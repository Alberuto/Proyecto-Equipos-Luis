using UnityEngine;

public class cogerInstrumento2A : MonoBehaviour {

    private GameObject playerObject;
    private PlayerTake player;
    private bool cogido = false;
    public int dupeMax = 4;

    void Start() {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject?.GetComponent<PlayerTake>();
        dupeMax = 14;
    }
    private void Update() {
        if (player?.espera == true && player.cogido == false) {
            player.coger = false;
        }
    }
    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("mano") && player?.coger == true && !player.cogido) {
            cogido = true;
            // 🆕 TUTORIAL 2A: Registrar objeto cogido
            AttackManagerTutorialObjetos2A tutorial = FindObjectOfType<AttackManagerTutorialObjetos2A>();
            if (tutorial != null) {
                tutorial.RegistrarObjetoCogido(this.tag);
                Debug.Log($"🎯 2A Tutorial: {this.tag} registrado");
            }
            // TU CÓDIGO EXISTENTE de duplicar y posicionar...
            GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
            if (objs.Length < dupeMax) {
                Transform padre = GetPadrePorTag(tag);
                if (padre) Instantiate(this, padre);
            }
            transform.SetParent(other.transform);
            SetPosicionMano(tag);
            player.añadirInstrumento(this.gameObject);
        }
    }
    private Transform GetPadrePorTag(string tag) {
        string path = $"ObjetosConSonido/{tag}";
        return GameObject.Find(path)?.transform;
    }
    private void SetPosicionMano(string tag) {
        // TUS POSICIONES EXISTENTES (copia de tu código original)
        switch (tag)  {
            case "C": transform.localPosition = new Vector3(0.015f, -0.021f, -0.479f); transform.localEulerAngles = new Vector3(270, 180, 0); break;
            case "C#": transform.localPosition = new Vector3(1.374f, -0.596f, -0.846f); transform.localEulerAngles = new Vector3(0, 0, 180); break;
            // ... resto de casos COPY/PASTE de tu código original
            default: break;
        }
    }
    public void soltar() {
        transform.SetParent(null);
        transform.localEulerAngles = Vector3.zero;
        player.eliminarInstrumento();
        cogido = false;
    }
    public bool objetoCogido() { return cogido; }
}
