using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class toroMovement : MonoBehaviour
{
    [SerializeField] public PlayerMovement player;
    private Vector3 target;
    private Vector3 toro; 
    public Transform posicionInicial;
    public float speed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = posicionInicial.position;
    }

    // Update is called once per frame
    void Update()
    {
        toro = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.position = Vector3.MoveTowards(toro, target, speed * Time.deltaTime);
        //transform.rotation = Quaternion.Inverse(transform.rotation);
        transform.LookAt(target);
        Vector3 rot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(rot.x, -rot.y, rot.z); // Invierte eje Y
        Debug.Log("rotacion toro: " + transform.rotation);

    }
}
