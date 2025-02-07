using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    NavMeshAgent zombie;
    [SerializeField] GameObject[] path;
    [SerializeField] int goal = 0;

    [SerializeField] Animator anim;
    [SerializeField] GameObject player;
    [SerializeField] float distance;
    [SerializeField] float visionArea = 10;
    [SerializeField] bool follow = false;
    float punchCooldown = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zombie = GetComponent<NavMeshAgent>();
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
            zombie.destination = path[goal].transform.position;
            anim.SetBool("isMoving", true);
            follow = false;
        }

        if (zombie.remainingDistance < 3 && !follow)
        {
            goal++;
            if (goal == path.Length)
            {
                goal = 0;
            }
            zombie.destination = path[goal].transform.position;
        }

        if (distance < 3 && follow)
        {
            anim.SetBool("isMoving", false);
            Punch();
        }
        else if (follow)
        {
            Detection();
        }
    }

    void Detection()
    {
        zombie.destination = player.transform.position;
        anim.SetBool("isMoving", true);
    }

    void Scream()
    {
        if (!follow)
        {
            anim.SetTrigger("hasScreamed");
            zombie.speed = 0;
            follow = true;
            Invoke("StartFollow", 2);
        }
    }

    void StartFollow()
    {        
        zombie.speed = 1.5f;
    }

    void Punch()
    {
        if (punchCooldown <= 0.1)
        {
            anim.SetTrigger("hasPunched");
        }
        punchCooldown += Time.deltaTime;
        if (punchCooldown >= 2.5) { punchCooldown = 0; }
    }
}
