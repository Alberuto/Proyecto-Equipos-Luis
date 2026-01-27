using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttackSelector : MonoBehaviour
{
    [Header("Paneles Principales")]
    public GameObject panelAtaques;
    public GameObject panelClasico;
    public GameObject panelDeathMetal;
    public GameObject panelDodecafonico;

    [Header("Botones Ataques (3 botones)")]
    public Button btnClasico;
    public Button btnDeathMetal;
    public Button btnDodecafonico;

    [Header("CORREGIDO: 4 ataques por categoria")]
    [Header("Clásico (4 ataques + Volver)")]
    public AudioClip[] audioClasico = new AudioClip[4];
    public string[] secuenciasClasico = new string[4];

    [Header("Death Metal (4 ataques + Volver)")]
    public AudioClip[] audioDeathMetal = new AudioClip[4];
    public string[] secuenciasDeathMetal = new string[4];

    [Header("Dodecafonico (4 ataques + Volver)")]
    public AudioClip[] audioDodeca = new AudioClip[4];
    public string[] secuenciasDodeca = new string[4];

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetupButtons();
        ShowPanel(panelAtaques);
    }

    void SetupButtons()
    {
        // 3 botones principales OK
        btnClasico.onClick.AddListener(() => ShowPanel(panelClasico));
        btnDeathMetal.onClick.AddListener(() => ShowPanel(panelDeathMetal));
        btnDodecafonico.onClick.AddListener(() => ShowPanel(panelDodecafonico));

        // Cada panel: 4 ataques + 1 Volver = 5 botones
        SetupPanelButtons(panelClasico, audioClasico, secuenciasClasico, SeleccionarAtaqueClasico);
        SetupPanelButtons(panelDeathMetal, audioDeathMetal, secuenciasDeathMetal, SeleccionarAtaqueDeathMetal);
        SetupPanelButtons(panelDodecafonico, audioDodeca, secuenciasDodeca, SeleccionarAtaqueDodeca);
    }

    void SetupPanelButtons(GameObject panel, AudioClip[] audios, string[] secuencias, System.Action<int> onAttackSelected)
    {
        // Primeros 4 botones = ataques [0,1,2,3]
        for (int i = 0; i < 4; i++)
        {
            Button btn = panel.transform.GetChild(i).GetComponent<Button>();
            int index = i;
            btn.onClick.AddListener(() => onAttackSelected(index));
        }

        // Último botón = Volver
        Button btnVolver = panel.transform.GetChild(4).GetComponent<Button>();
        btnVolver.onClick.AddListener(() => ShowPanel(panelAtaques));
    }

    void ShowPanel(GameObject panel)
    {
        panelAtaques.SetActive(false);
        panelClasico.SetActive(false);
        panelDeathMetal.SetActive(false);
        panelDodecafonico.SetActive(false);
        panel.SetActive(true);
    }

    void SeleccionarAtaqueClasico(int index)
    {
        PlayAttack(audioClasico[index], secuenciasClasico[index]);
        ShowPanel(panelAtaques);
    }

    void SeleccionarAtaqueDeathMetal(int index)
    {
        PlayAttack(audioDeathMetal[index], secuenciasDeathMetal[index]);
        ShowPanel(panelAtaques);
    }

    void SeleccionarAtaqueDodeca(int index)
    {
        PlayAttack(audioDodeca[index], secuenciasDodeca[index]);
        ShowPanel(panelAtaques);
    }

    void PlayAttack(AudioClip audio, string secuencia)
    {
        if (audio != null)
            audioSource.PlayOneShot(audio);

        Debug.Log("Ataque: " + secuencia);
    }
}
