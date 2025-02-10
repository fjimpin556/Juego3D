using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour
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
        if (HistoryController.antidoto)
        {
            if (distancia < 5 && !open)
            {
                anim.SetBool("isOpening", true);
                aud.PlayOneShot(openDD);                
                Invoke("goToCredits", 1);
                open = true;
            }            
        }
    }

    void goToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
