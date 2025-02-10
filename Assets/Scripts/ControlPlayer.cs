using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ControlPlayer : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 7;

    public float groundDrag = 4;

    public float jumpForce = 12;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.4f;
    bool readyToJump;
    [SerializeField] Slider lifeBar;
    float health = 100;
    public static bool playerDeath = false;
    bool death = false;

    // [HideInInspector] public float walkSpeed = 7;
    // [HideInInspector] public float sprintSpeed = 10;

    [Header("Balas")]
    public static int bulletP = 0;
    [SerializeField] TMP_Text bulletCount;
    [SerializeField] GameObject bulletCont;

    [Header("Teclas")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode gunKey = KeyCode.Alpha1;
    [SerializeField] GameObject Pistol;
    [SerializeField] GameObject pistolFloor;
    public bool pistoltrue = false;

    [Header("Comprobar suelo")]
    public float playerHeight = 2;
    public LayerMask whatIsGround;
    bool grounded;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Historia")]
    public static bool canMove = false;
    public static bool canPistol = false;
    public HistoryController HistC;
    public static int killCount = 0;
    [SerializeField] GameObject miniMap;
    [SerializeField] GameObject Map;
    bool showingMap = false;

    [Header("Animator")]
    public AnimatorController animControl;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        if (canMove)
        {
            MyInput();
            SpeedControl();
            bulletCount.text = bulletP + "";
        }
        else if (canPistol)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                HistC.QuitDialogue1();
                Invoke("takePistol", 1.2f);
                Invoke("startMove", 2);
                Invoke("AgentPart", 5);
            }
        }
        else if (HistoryController.historyCount == 6)
        {
            miniMap.SetActive(true);
        }

        if (killCount >= 40 && !HistoryController.infected)
        {
            HistoryController.historyCount = 31;
        }

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(gunKey))
        {
            if (pistoltrue)
            {
                Pistol.SetActive(false);
                pistoltrue = false;
            }
            else
            {
                Pistol.SetActive(true);
                pistoltrue = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (showingMap)
            {
                Map.SetActive(false);
                showingMap = false;
            }
            else
            {
                Map.SetActive(true);
                showingMap = true;
            }
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    void takePistol()
    {
        Pistol.SetActive(true);
        pistoltrue = true;
        pistolFloor.SetActive(false);
        bulletP = 10;
        bulletCount.text = bulletP + "";
        bulletCont.SetActive(true);
    }

    void startMove()
    {
        canMove = true;
    }

    void AgentPart()
    {
        HistoryController.historyCount = 9;
    }

    public void Damage()
    {
        if (!death)
        {
            health -= 10;
            lifeBar.value = health;
            if (health <= 0)
            {
                playerDeath = true;
                death = true;
                Invoke("goToMenu", 3);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "random1")
        {
            HistoryController.historyCount = 20;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "BulletRec")
        {
            animControl.Pick();
            Destroy(other.gameObject);
            bulletP += 4;
        }
        if (other.gameObject.tag == "BulletDrop")
        {
            animControl.Pick();
            Destroy(other.gameObject);
            bulletP += 1;
        }
        if (other.gameObject.tag == "takeF")
        {
            animControl.Pick();
            Destroy(other.gameObject);
            HistoryController.finger = true;
        }
        if (other.gameObject.tag == "antidoto")
        {
            animControl.Pick();
            Destroy(other.gameObject);
            HistoryController.antidoto = true;
        }
        if (other.gameObject.tag == "BulletEnemy")
        {
            Damage();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Cure")
        {
            health += 25;
            if (health > 100) { health = 100; }
            lifeBar.value = health;
            animControl.Pick();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "EndDoor")
        {
            if (HistoryController.antidoto)
            {
                Invoke("goCredits", 2);
            }
        }
        if (other.gameObject.tag == "SecurityDoor" && !HistoryController.finger)
        {
            if (HistoryController.historyCount == 22) { HistoryController.historyCount = 23; }
            if (HistoryController.historyCount == 42) { HistoryController.historyCount = 43; }
        }
    }

    void goToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void goCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
