using UnityEngine;

public class VinylCircularOrbit : MonoBehaviour
{
    [Header("Centro de órbita")]
    public Transform orbitCenter;

    [Header("Órbita Normal")]
    public float radius = 2f;
    public float speed = 40f;
    public bool clockwise = true;

    [Header("Giro del vinilo")]
    public float spinSpeed = 720f;

    private float currentAngle;
    private Vector3 initialEuler;
    private float fixedY;  // ← NUEVO: Y FIJO

    void Start()
    {
        if (orbitCenter == null)
            orbitCenter = transform.parent;

        // FIJA Y DESDE EL PRINCIPIO
        fixedY = transform.position.y;  // ← AQUÍ
        initialEuler = transform.eulerAngles;

        Vector3 initialOffset = transform.position - orbitCenter.position;
        currentAngle = Mathf.Atan2(initialOffset.z, initialOffset.x);
    }

    void Update()
    {
        if (orbitCenter == null) return;

        // Órbita suave (Y FIJO)
        float angleDelta = speed * Mathf.Deg2Rad * Time.deltaTime * (clockwise ? 1f : -1f);
        currentAngle += angleDelta;

        Vector3 targetPos = orbitCenter.position + new Vector3(
            Mathf.Cos(currentAngle) * radius,
            fixedY,  // ← CAMBIADO: Y FIJO SIEMPRE
            Mathf.Sin(currentAngle) * radius
        );

        transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime * 8f);

        // Giro rápido del vinilo
        Vector3 euler = initialEuler;
        euler.y += spinSpeed * Time.deltaTime;
        transform.eulerAngles = euler;
    }
}
