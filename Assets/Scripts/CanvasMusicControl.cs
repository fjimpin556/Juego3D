using UnityEngine;

public class CanvasMusicControl : MonoBehaviour
{
    [SerializeField] AudioClip secondSong;
    AudioSource aud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HistoryController.infected)
        {
            aud.PlayOneShot(secondSong);
        }
    }
}
