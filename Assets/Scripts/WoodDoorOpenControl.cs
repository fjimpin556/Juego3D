using UnityEngine;

public class WoodDoorOpenControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    float distancia = 10;
    [SerializeField] Animator anim;
    bool open = false;

    [SerializeField] AudioClip close, openD;
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
        if (distancia < 2 && Input.GetKeyDown(KeyCode.E) && open)
        {
            anim.SetBool("isOpening", false);
            aud.PlayOneShot(close);
            open = false;
        }
        else if (distancia < 2 && Input.GetKeyDown(KeyCode.E) && !open)
        {
            anim.SetBool("isOpening", true);
            aud.PlayOneShot(openD);
            open = true;
        }
    }
}
