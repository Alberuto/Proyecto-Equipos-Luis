using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance;

    public int prueba = 0; // variable para probar la carga de escenas y el cambio de enemigo actual, se puede eliminar despues de las pruebas
    // variable para almacenar el enemigo actual, se actualiza cada vez que se carga una escena y se puede acceder desde otros scripts para actualizar la vida del enemigo en la UI
    private string enemyActual = "";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        /*// prueba
        if (prueba == 0)
        {
            enemy("Kiko");
        }
        if (prueba == 1)
        {
            enemy("Cigala");
        }
        if (prueba == 2)
        {
            enemy("Fary");
        }*/
        enemy("Nivel1-Defensa");
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        enemy(sceneName);
    }
    // método para actualizar el enemigo actual según la escena cargada, se llama cada vez que se carga una escena
    private void enemy(string scene)
    {
        // cambiar el valor de los if cuando se sepan los nombres definitivos de as escenas, por ahora se usan los nombres de los enemigos para probar el funcionamiento
        if (scene == "Nivel1-Defensa")
        {
            enemyActual = "kiko";
        }
        else if (scene == "Nivel2-Defensa")
        {
            enemyActual = "cigala";
        }
        else if (scene == "Nivel3-Defensa")
        {
            enemyActual = "fary";
        }
    }
    // método para obtener el enemigo actual, se puede llamar desde otros scripts para actualizar la vida del enemigo en la UI
    public string getEnemyActual()
    {
        return enemyActual;
    }
}
