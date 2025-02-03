using UnityEngine;

public class DoorOpenControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    float distancia = 10;
    [SerializeField] Animator anim;
    bool open = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distancia = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (distancia < 1 && Input.GetKeyDown(KeyCode.E) && open)
        {
            anim.SetBool("isOpening", false);
            open = false;
        }
        else if (distancia < 1 && Input.GetKeyDown(KeyCode.E) && !open)
        {
            anim.SetBool("isOpening", true);
            open = true;
        }
    }
}
