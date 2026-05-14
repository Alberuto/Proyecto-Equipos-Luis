using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class parche : MonoBehaviour {

    [SerializeField] private List<GameObject> gameManagers;
    void Awake() {
        // Buscar y destruir GameManager2 si existe
        GameObject.FindGameObjectsWithTag("GameManager", gameManagers); // o el nombre exacto
        foreach (GameObject gameManager in gameManagers) {
            gameManager.TryGetComponent<GameManager3>(out GameManager3 gameManager3);
            if (gameManager3 == null) {
                Destroy(gameManager);
            }
        }
    }
}