using UnityEngine;

public class VinylCircularOrbit : MonoBehaviour
{
    [Header("Centro de órbita")]
    public Transform orbitCenter;

    [Header("Órbita Normal")]
    public float radius = 2f;
    public float speed = 40f;
    public bool clockwise = true;

    [Header("MOVIMIENTO CENTRO L→D")]
    public float centerMoveSpeed = 1f;     // Velocidad izquierda-derecha
    public float centerMoveDistance = 5f;  // Distancia total recorrido

    [Header("Giro del vinilo")]
    public float spinSpeed = 720f;

    private float currentAngle;
    private Vector3 initialEuler;
    private float fixedY;
    private float centerMoveOffset;  // ← NUEVO: offset horizontal del centro

    void Start()
    {
        if (orbitCenter == null)
            orbitCenter = transform.parent;

        fixedY = transform.position.y;
        initialEuler = transform.eulerAngles;

        Vector3 initialOffset = transform.position - orbitCenter.position;
        currentAngle = Mathf.Atan2(initialOffset.z, initialOffset.x);
    }

    void Update()
    {
        if (orbitCenter == null) return;

        // MOVIMIENTO CENTRO Izquierda → Derecha (seno suave)
        centerMoveOffset = Mathf.Sin(Time.time * centerMoveSpeed) * (centerMoveDistance * 0.5f);

        // POSICIÓN CENTRO DINÁMICA
        Vector3 dynamicCenter = orbitCenter.position + Vector3.right * centerMoveOffset;

        // Órbita alrededor del centro MÓVIL
        float angleDelta = speed * Mathf.Deg2Rad * Time.deltaTime * (clockwise ? 1f : -1f);
        currentAngle += angleDelta;

        Vector3 targetPos = dynamicCenter + new Vector3(
            Mathf.Cos(currentAngle) * radius,
            fixedY,
            Mathf.Sin(currentAngle) * radius
        );

        transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime * 8f);

        // Giro rápido del vinilo
        Vector3 euler = initialEuler;
        euler.y += spinSpeed * Time.deltaTime;
        transform.eulerAngles = euler;
    }
}
