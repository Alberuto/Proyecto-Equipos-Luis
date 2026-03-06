using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField] private GameObject canvasSettings;
    [SerializeField] private GameObject panelRanking;
    [SerializeField] private GameObject panelNombre;
    [SerializeField] private TMP_InputField inputNombre;
    public Transform rankingContainer;
    public GameObject rankingEntryPrefab;


    private void Awake() {
        // GUARDAR nivel actual ANTES de borrar
        int nivelActual = PlayerPrefs.GetInt("NivelActual", 0);
        string nombre = PlayerPrefs.GetString("NombreJugador", "Anónimo");

        // BORRAR TODO
        PlayerPrefs.DeleteAll();

        // RESTAURAR nivel actual (indestructible)
        PlayerPrefs.SetInt("NivelActual", nivelActual);
        PlayerPrefs.SetString("NombreJugador", nombre);
        PlayerPrefs.Save();

        // 🔥 INICIALIZAR ScoreManager DESPUÉS de restaurar PlayerPrefs
        if (ScoreManager.Instance != null) {
            ScoreManager.Instance.Inicializar();
        }
        Debug.Log($"🆕 MainMenu: Nivel guardado = {nivelActual}");
    }
    public void CargarNivelProgreso() {
        if (ScoreManager.Instance.nombreJugador == "Anónimo" ||
            string.IsNullOrEmpty(ScoreManager.Instance.nombreJugador)) {
            panelNombre.SetActive(true);  // ← Pedir nombre
        }
        else {

            int nivel = PlayerPrefs.GetInt("NivelActual", 0);
            Debug.Log($"🚀 CONTINUAR → Nivel {nivel}");

            switch (nivel) {
                default: SceneManager.LoadScene("Nivel0"); break;
                case 0: SceneManager.LoadScene("Nivel0"); break;
                case 1: SceneManager.LoadScene("Nivel1"); break;
                case 2: SceneManager.LoadScene("Nivel2"); break;
                case 3: SceneManager.LoadScene("Nivel2"); break;
                case 4: SceneManager.LoadScene("Nivel2"); break;
                case 5: SceneManager.LoadScene("Nivel2"); break;
            }
        }
    }
    public void ConfirmarNombre() {
        string nombre = inputNombre.text;
        if (string.IsNullOrEmpty(nombre)) nombre = "Jugador 1";

        ScoreManager.Instance.nombreJugador = nombre;  // ← Persiste
        inputNombre.text = "";  // Limpiar
        panelNombre.SetActive(false);

        CargarNivelProgreso();  // ← Ahora sí carga
    }
    public void NuevaPartida() {
        PlayerPrefs.DeleteAll();
        canvasSettings.SetActive(false);
    }
    public void AbrirCanvasSettings() {
        if (canvasSettings) canvasSettings.SetActive(true);
    }
    public void AbrirCanvasRanking() {
        panelRanking.SetActive(true);
    }
    public void CerrarCanvasRanking() {
        panelRanking.SetActive(false);
    }
    public void CerrarPanelNombre() { 
        ConfirmarNombre();
        panelNombre.SetActive(false); 
    }
    //FASE 3: RANKING FUNCIONAL
    public void Ranking() {
        MostrarRanking();
    }
    //RANKING METHODS
    public void MostrarRanking() {

        RankingData data = new RankingData();
        data.Load();

        //Limpiar
        foreach (Transform child in rankingContainer) {
            Destroy(child.gameObject);
        }
        // Generar TOP 10
        for (int i = 0; i < data.top10.Count; i++) {
            GameObject entry = Instantiate(rankingEntryPrefab, rankingContainer);
            TMP_Text texto = entry.GetComponentInChildren<TMP_Text>();
            texto.text = $"{i + 1}. {data.top10[i].nombre} - {data.top10[i].scoreTotal}pts (N:{data.top10[i].nivelMaximo})";
        }
        panelRanking.SetActive(true);
    }
    public static void GuardarRankingDirecto(string nombre, int score, int nivel) {

        RankingData data = new RankingData();
        data.Load(); //para que sea acumulativo en cada ejecucion
        data.AddEntry(nombre, score, nivel);
    }
    //CLASES JSON
    [System.Serializable]
    public class RankingEntry {

        public string nombre;
        public int scoreTotal;
        public int nivelMaximo;
        public string fecha;
    }

    [System.Serializable]
    public class RankingData {

        public List<RankingEntry> top10 = new List<RankingEntry>();

        public void AddEntry(string nombre, int score, int nivel)  {
            top10.Add(new RankingEntry {
                nombre = nombre,
                scoreTotal = score,
                nivelMaximo = nivel,
                fecha = System.DateTime.Now.ToString("dd/MM HH:mm")
            });
            top10.Sort((a, b) => b.scoreTotal.CompareTo(a.scoreTotal));
            if (top10.Count > 10) top10.RemoveRange(10, top10.Count - 10);
            Save();
        }
        public void Save() {
            string path = Application.persistentDataPath + "/ranking.json";
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(path, json);
            Debug.Log("Ranking guardado en: " + path);
            Debug.Log("Contenido:\n" + json);
        }
        public void Load()  {
            string path = Application.persistentDataPath + "/ranking.json";
            if (File.Exists(path)) {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, this);
            }
        }
    }
}