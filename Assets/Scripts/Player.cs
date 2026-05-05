using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] Rigidbody rb;
    public Joystick joystick;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = joystick.Horizontal; 
        float z = joystick.Vertical; 
        Vector3 move = new Vector3(x, 0, z) * speed; 
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }
}
