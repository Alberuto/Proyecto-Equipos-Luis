using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {
    public void CargarNivel(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
