using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AttackSelector : MonoBehaviour { 

    [Header("Paneles Principales")]
    public GameObject panelAtaques;
    public GameObject panelClasico;
    public GameObject panelDeathMetal;
    public GameObject panelDodecafonico;
    public GameObject panelPista;

    [Header("Panel Pista")]
    public TextMeshProUGUI textoPista;
    public Button btnEmpezarAtaque;
    public Button btnVolverClasico;    //Nuevos botones por si se arrepiente por la dificultad del ataque elegido
    public Button btnVolverDeathMetal;
    public Button btnVolverDodeca;

    [Header("Botones Ataques (3 botones)")]
    public Button btnClasico;
    public Button btnDeathMetal;
    public Button btnDodecafonico;

    [Header("Clásico (4 ataques)")]
    public AudioClip[] audioClasico = new AudioClip[4];
    public string[] secuenciasClasico = new string[4];
    public int[] damageClasico = new int[4];

    [Header("Death Metal (4 ataques)")]
    public AudioClip[] audioDeathMetal = new AudioClip[4];
    public string[] secuenciasDeathMetal = new string[4];
    public int[] damageDeathMetal = new int[4];

    [Header("Dodecafonico (4 ataques)")]
    public AudioClip[] audioDodeca = new AudioClip[4];
    public string[] secuenciasDodeca = new string[4];
    public int[] damageDodecafonico = new int[4];


    private AudioSource audioSource;
    private string pistaActual;
    private int damageBaseActual = 1;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        SetupButtons();
        ShowPanel(panelAtaques);
    }
    void SetupButtons() {
        btnClasico.onClick.AddListener(() => ShowPanel(panelClasico));
        btnDeathMetal.onClick.AddListener(() => ShowPanel(panelDeathMetal));
        btnDodecafonico.onClick.AddListener(() => ShowPanel(panelDodecafonico));

        SetupPanelButtons(panelClasico, audioClasico, secuenciasClasico, SeleccionarAtaqueClasico);
        SetupPanelButtons(panelDeathMetal, audioDeathMetal, secuenciasDeathMetal, SeleccionarAtaqueDeathMetal);
        SetupPanelButtons(panelDodecafonico, audioDodeca, secuenciasDodeca, SeleccionarAtaqueDodeca);

        if (btnEmpezarAtaque != null)
            btnEmpezarAtaque.onClick.AddListener(OnEmpezarAtaque);
        //ATAJOS DEL PANEL PISTA
        if (btnVolverClasico != null)
            btnVolverClasico.onClick.AddListener(() => ShowPanel(panelClasico));
        if (btnVolverDeathMetal != null)
            btnVolverDeathMetal.onClick.AddListener(() => ShowPanel(panelDeathMetal));
        if (btnVolverDodeca != null)
            btnVolverDodeca.onClick.AddListener(() => ShowPanel(panelDodecafonico));
    }
    void SetupPanelButtons(GameObject panel, AudioClip[] audios, string[] secuencias, System.Action<int> onAttackSelected) {
        for (int i = 0; i < 4; i++) {
            Button btn = panel.transform.GetChild(i).GetComponent<Button>();
            int index = i;
            btn.onClick.AddListener(() => onAttackSelected(index));
        }
        Button btnVolver = panel.transform.GetChild(4).GetComponent<Button>();
        btnVolver.onClick.AddListener(() => ShowPanel(panelAtaques));
    }
    void ShowPanel(GameObject panel) {

        panelAtaques.SetActive(false);
        panelClasico.SetActive(false);
        panelDeathMetal.SetActive(false);
        panelDodecafonico.SetActive(false);
        panelPista.SetActive(false);
        panel.SetActive(true);
    }
    void SeleccionarAtaqueClasico(int index) {
        PlayAttack(audioClasico[index], secuenciasClasico[index], damageClasico[index]);
        PlayerPrefs.SetInt("AttackValue", damageClasico[index]);
    }
    void SeleccionarAtaqueDeathMetal(int index) {
        PlayAttack(audioDeathMetal[index], secuenciasDeathMetal[index],damageDeathMetal[index]);
        PlayerPrefs.SetInt("AttackValue", damageDeathMetal[index]);
    }
    void SeleccionarAtaqueDodeca(int index) {
        PlayAttack(audioDodeca[index], secuenciasDodeca[index], damageDodecafonico[index]);
        PlayerPrefs.SetInt("AttackValue", damageDodecafonico[index]);
    }
    void PlayAttack(AudioClip audio, string secuencia, int damage) {

        if (audio != null)
            audioSource.PlayOneShot(audio);

        pistaActual = secuencia;
        damageBaseActual = damage;

        if (textoPista != null)
            textoPista.text = pistaActual;
        
        ShowPanel(panelPista);
    }
    void OnEmpezarAtaque() {

        panelPista.SetActive(false);
        Debug.Log("¡A atacar con: " + pistaActual); //pasamos el ataque seleccionado al attack manager donde lo convertirá a lista de notas y lo comparará con las entradas del jugador

        AttackManager attackMgr = FindObjectOfType<AttackManager>();
        if (attackMgr != null) {
            attackMgr.IniciarAtaque(pistaActual,damageBaseActual);
        }
        // escena coger objetos (2.A2 y 3)
        AttackManagerSecuencia3 attackMgr3 = FindObjectOfType<AttackManagerSecuencia3>();
        if (attackMgr3 != null) {
            attackMgr3.IniciarAtaque(pistaActual, damageBaseActual);
        }
        // escena de lulu (4.A2 y 5)
        AttackManagerSecuencia5 attackMgr5 = FindObjectOfType<AttackManagerSecuencia5>();
        if (attackMgr5 != null) {
            attackMgr5.IniciarAtaque(pistaActual, damageBaseActual);
        }
    }
}