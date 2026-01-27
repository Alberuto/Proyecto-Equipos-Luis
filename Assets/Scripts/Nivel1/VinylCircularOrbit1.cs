using UnityEngine;

public class VinylCircularOrbit1 : MonoBehaviour
{
    [Header("Centro de órbita")]
    public Transform orbitCenter;

    [Header("Órbita Normal")]
    public float radius = 2f;
    public float speed = 40f;
    public bool clockwise = true;

    private float currentAngle;
    private Vector3 initialEuler;  // ← NUEVO: guarda rotación inicial del sprite

    void Start()
    {
        if (orbitCenter == null)
            orbitCenter = transform.parent;

        // GUARDA ROTACIÓN INICIAL del sprite (¡EVITA FLIP!)
        initialEuler = transform.eulerAngles;

        Vector3 initialOffset = transform.position - orbitCenter.position;
        currentAngle = Mathf.Atan2(initialOffset.z, initialOffset.x);
    }

    void Update()
    {
        if (orbitCenter == null) return;

        // ÓRBITA: solo posición (SIN tocar rotación)
        float angleDelta = speed * Mathf.Deg2Rad * Time.deltaTime * (clockwise ? 1f : -1f);
        currentAngle += angleDelta;

        Vector3 targetPos = orbitCenter.position + new Vector3(
            Mathf.Cos(currentAngle) * radius,
            transform.position.y,
            Mathf.Sin(currentAngle) * radius
        );

        transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime * 8f);

        // GIRO COMO DISCO: solo eje Y, rotación LOCAL
        transform.Rotate(0, 180 * Time.deltaTime, 0, Space.Self);

        // MANTIENE ORIENTACIÓN INICIAL (¡NO FLIP!)
        Vector3 euler = initialEuler;
        euler.y += 180 * Time.deltaTime;  // Solo suma al Y
        transform.eulerAngles = euler;
    }
}
