using UnityEngine;
using UnityEngine.AI;

public class ZombieFirstControl : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Animator anim;

    [SerializeField] GameObject player;
    [SerializeField] float visionArea = 5;
    float distance;
    float screamCooldown = 0;

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
    }

    void Scream()
    {
        if (screamCooldown <= 0.1)
        {
            anim.SetTrigger("hasScreamed");
        }
        screamCooldown += Time.deltaTime;
        if (screamCooldown >= 4) {screamCooldown = 0;}
    }
}
