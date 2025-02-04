using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform goal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
