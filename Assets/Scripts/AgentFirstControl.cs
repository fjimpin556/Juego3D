using UnityEngine;
using UnityEngine.AI;

public class AgentFirstControl : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform goal;    
    [SerializeField] Animator anim;
    

    bool esperando;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();        
        startMoving();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance > 1)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    void startMoving()
    {
        agent.destination = goal.position;
    }
}
