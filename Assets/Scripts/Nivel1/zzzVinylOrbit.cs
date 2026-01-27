using UnityEngine;

public class VinylOrbit : MonoBehaviour {

    public Transform center;      // Centro de la órbita (normalmente OrbitsRoot o KikoRoot)
    public Transform vinyl;       // El disco
    public float radius = 1.5f;   // Radio normal (concéntrico)
    public float furyRadius = 2f; // Radio en modo furia (más loco / excéntrico)
    public float speed = 50f;     // Velocidad en grados/segundo
    public float initialAngleDeg; // Offset inicial para repartirlos en el círculo
    public bool isFury;           // Lo activas desde tu state machine de Kiko

    float _angleRad;

    void Start() {
        _angleRad = initialAngleDeg * Mathf.Deg2Rad;
    }
    void Update() {

        if (center == null || vinyl == null) return;

        float currentRadius = isFury ? furyRadius : radius;

        _angleRad += speed * Mathf.Deg2Rad * Time.deltaTime;

        Vector3 offset = new Vector3(Mathf.Cos(_angleRad), 0f, Mathf.Sin(_angleRad)) * currentRadius;
        vinyl.position = center.position + offset;
    }
}