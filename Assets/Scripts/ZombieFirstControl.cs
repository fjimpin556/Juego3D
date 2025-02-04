using UnityEngine;
using UnityEngine.AI;

public class ZombieFirstControl : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Animator anim;
    
    [SerializeField] GameObject player;
    [SerializeField] float visionArea = 5;
    float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < visionArea)
        {
            Scream();
        }
        else 
        {
            goBack();
        }
    }

    void Scream()
    {}

    void goBack(){}
}
