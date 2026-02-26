using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour {

    [SerializeField] private Image flashImage;

    private Color colorBase;

    void Awake() {
        if (flashImage == null) flashImage = GetComponent<Image>();
        colorBase = flashImage.color;
        colorBase.a = 0f;
        flashImage.color = colorBase;
    }

    public void FlashCombo(int nivelCombo) {
        StartCoroutine(AnimarFlash(nivelCombo));
    }
    private IEnumerator AnimarFlash(int nivel) {

        Color colorFlash = ObtenerColorPorNivel(nivel);

        // Flash IN (0.125s)
        float tiempo = 0f;
        while (tiempo < 0.125f) {
            tiempo += Time.deltaTime;
            colorFlash.a = Mathf.Lerp(0f, 1f, tiempo / 0.125f);
            flashImage.color = colorFlash;
            yield return null;
        }
        // Flash OUT (0.125s)  
        tiempo = 0f;
        while (tiempo < 0.125f) {
            tiempo += Time.deltaTime;
            colorFlash.a = Mathf.Lerp(1f, 0f, tiempo / 0.125f);
            flashImage.color = colorFlash;
            yield return null;
        }
        flashImage.color = colorBase;
    }
    private Color ObtenerColorPorNivel(int nivel) {

        if (nivel == 10) {
            return new Color(1f, 0f, 1f, 1f);  // Rosa (10-12)
        }
        else if (nivel == 11) {
            return new Color(1f, 0.5f, 0f, 1f); // Naranja (11)
        }
        else if (nivel == 12) {
            return new Color(1f, 0f, 0f, 1f);   // Rojo (12)
        }
        else if (nivel >= 7 && nivel <= 9)  {
            return new Color(0.5f, 0f, 1f, 1f); // Morado (7-9)
        }
        else if (nivel >= 4 && nivel <= 6)   {
            return new Color(0f, 0.7f, 1f, 1f); // Azul (4-6)
        }
        else if (nivel >= 1 && nivel <= 3)  {
            return new Color(0f, 1f, 0f, 1f);   // Verde (1-3)
        }
        else {
            return Color.white;
        }
    }
}
