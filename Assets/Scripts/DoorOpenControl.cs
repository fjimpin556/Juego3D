using UnityEngine;

public class DoorOpenControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    float distancia = 10;
    [SerializeField] Animator anim;
    bool open = false;
    public static bool key1 = false;

    [SerializeField] AudioClip openT;
    AudioSource aud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        distancia = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (key1)
        {
            if (distancia < 1 && Input.GetKeyDown(KeyCode.E) && open)
            {
                anim.SetBool("isOpening", false);
                aud.PlayOneShot(openT);
                open = false;
            }
            else if (distancia < 1 && Input.GetKeyDown(KeyCode.E) && !open)
            {
                anim.SetBool("isOpening", true);
                aud.PlayOneShot(openT);
                open = true;
            }
        }
    }
}
