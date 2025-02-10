using UnityEngine;

public class Wood2DoorOpenControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    float distancia = 10;
    [SerializeField] Animator anim;
    bool open = false;
    [SerializeField] GameObject[] zombie;

    [SerializeField] AudioClip openDD;
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
        if (ControlPlayer.killCount == zombie.Length)
        {
            anim.SetBool("isOpening", true);
            aud.PlayOneShot(openDD);
            open = true;
            ControlPlayer.killCount = 0;
        }        
    }
}
