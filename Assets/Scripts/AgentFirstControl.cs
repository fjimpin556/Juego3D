using UnityEngine;
using UnityEngine.AI;

public class AgentFirstControl : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform goal;
    [SerializeField] Transform goal2;
    [SerializeField] Animator anim;
    [SerializeField] GameObject player;


    bool esperando = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
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
            if(!esperando)
            {
                HistoryController.historyCount = 12;
                esperando = true;
            }
        }
    }

    public void startMoving()
    {
        agent.destination = goal.position;
        esperando = false;
    }

    public void startGoing()
    {
        agent.destination = goal2.position;        
        esperando = false;
        Invoke("disappear", 2);
    }
    
    void disappear()
    {
        Destroy(gameObject);
    }
    
}
