using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Animator anim;
    [SerializeField] float jumpCooldown = 1.2f;
    float lastJump = 0;
    public ControlPlayer CP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");

        if (ControlPlayer.canMove)
        {
            if (movementX != 0 || movementZ != 0)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && (Time.time - lastJump > jumpCooldown))
            {
                lastJump = Time.time;
                anim.SetTrigger("hasJumped");
            }
        }
        else if (ControlPlayer.canPistol && Input.GetKeyDown(KeyCode.E))
        {
            ControlPlayer.canPistol = false;
            anim.SetTrigger("hasPistoled");
        }

        if (CP.pistoltrue)
        {
            anim.SetBool("isPistol", true);
        }
        else
        {
            anim.SetBool("isPistol", false);
        }

        if (ControlPlayer.playerDeath)
        {
            anim.SetTrigger("hasDied");
            ControlPlayer.playerDeath = false;
            ControlPlayer.canMove = false;
            ControlPlayer.canPistol = false;
        }
    }

    public void Pick()
    {
        anim.SetTrigger("hasPicked");        
    }
}
