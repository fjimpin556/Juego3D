using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float power = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.TransformDirection(Vector3.down) * power, ForceMode.Impulse);

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
        if (other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
