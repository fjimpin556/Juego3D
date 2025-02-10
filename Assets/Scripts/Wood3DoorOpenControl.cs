using UnityEngine;

public class Wood3DoorOpenControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    float distancia = 10;
    [SerializeField] Animator anim;
    bool open = false;    

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
        distancia = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (HistoryController.finger)
        {
            if (distancia < 5)
            {
                anim.SetBool("isOpening", true);
                aud.PlayOneShot(openDD);
                open = true;
                HistoryController.finger = false;
                HistoryController.historyCount = 49;
            }            
        }
    }
}
