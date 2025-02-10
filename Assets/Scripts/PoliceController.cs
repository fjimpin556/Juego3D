using UnityEngine;
using UnityEngine.AI;

public class PoliceController : MonoBehaviour
{
    NavMeshAgent police;
    [SerializeField] GameObject[] path;
    [SerializeField] int goal = 0;

    [SerializeField] Animator anim;
    [SerializeField] GameObject player;
    public ControlPlayer CP;
    [SerializeField] float distance;
    [SerializeField] float visionArea = 10;
    [SerializeField] bool follow = false;
    float punchCooldown = 0;
    [SerializeField] GameObject bulletDrop;
    [SerializeField] GameObject takeFinger;

    bool death = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        police = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (!death)
        {
            if (distance < visionArea)
            {
                follow = true;
                police.destination = player.transform.position;
                anim.SetBool("isMoving", true);
            }
            else
            {
                police.destination = path[goal].transform.position;
                anim.SetBool("isMoving", true);
                follow = false;
            }

            if (police.remainingDistance < 3 && !follow)
            {
                goal++;
                if (goal == path.Length)
                {
                    goal = 0;
                }
                police.destination = path[goal].transform.position;
            }

            if (distance < 3 && follow)
            {
                anim.SetBool("isMoving", false);
                Punch();
            }
        }
    }

    void Punch()
    {
        if (punchCooldown <= 0 && !death)
        {
            anim.SetTrigger("hasPunched");
            Invoke("DamagePlayer", 1);
        }
        punchCooldown += Time.deltaTime;
        if (punchCooldown >= 2.5) { punchCooldown = 0; }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (!death)
            {
                anim.SetTrigger("hasDied");
                Destroy(other.gameObject);
                death = true;
                if (!HistoryController.infected)
                {
                    ControlPlayer.killCount += 10;
                }
                else if (HistoryController.infected)
                {
                    ControlPlayer.killCount += 1;
                }
                Instantiate(bulletDrop, transform.position, transform.rotation);
                if (ControlPlayer.killCount < 39)
                {
                    Invoke("Dying", 3);
                }
                else
                {
                    takeFinger.SetActive(true);
                }
            }

        }
    }

    void Dying()
    {
        Destroy(gameObject);
    }

    void DamagePlayer()
    {
        if (distance < 2.5f)
        {
            CP.Damage();
        }
    }
}
