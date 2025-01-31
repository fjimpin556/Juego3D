using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float power = 150;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.TransformDirection(Vector3.forward) * power, ForceMode.Impulse);

        Invoke("Clean", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Clean()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
