using UnityEngine;

public class FireControl : MonoBehaviour
{
    [SerializeField] ParticleSystem fire;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fire.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            fire.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            fire.Stop();
        }
    }
}
