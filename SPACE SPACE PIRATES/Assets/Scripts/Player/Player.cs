using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Pop Ups?")]
    [SerializeField] public GameObject MenuPanel;


    public PlayerInputSet input { get; private set; }
    public StateMachine stateMachine { get; private set; }
    public PlayerStateIdle idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerMoveDownState down_moveState { get; private set; }
    public PlayerMoveUpState up_moveState { get; private set; }





    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public ParticleSystem dustRunning;

    [Header("Player Stats")]
    public float maxHealth = 100;
    public float health;
    [Space]


    [Header("Movement Details")]
    public float moveSpeed;
    public Vector2 moveInput;
    [Space]

    [Header("Death")]
    public GameManager gameManager;


    public bool isInMenu = true;


    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;







    public void Awake()
    {
        input = new PlayerInputSet();
        stateMachine = new StateMachine();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dustRunning = GetComponentInChildren<ParticleSystem>();



        idleState = new PlayerStateIdle(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        down_moveState = new PlayerMoveDownState(this, stateMachine, "MoveDown");
        up_moveState = new PlayerMoveUpState(this, stateMachine, "MoveUp");

        isInMenu = false;
        MenuPanel.SetActive(isInMenu);


    }

    public void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

    }

    public void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Intilize(idleState);
    }

    private void Update()
    {
        stateMachine.UpdateActiveState();

        //if(health <= 0)
        //{
        //    Die();
        //}
    }

    public void SetVelocity(float xVelocity, float yVelocity) {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }


    private void HandleCollisionDetection() {
        //GroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        //WallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDir,wallCheckDistance,whatIsGround);
    }

    private void HandleFlip(float xVelocity) {
        if (xVelocity > 0 && facingRight == false) {
            Flip();
        }
        else if (xVelocity < 0 && facingRight == true) {
            Flip();
        }

    }


    public void TakeDamage(float amount)
    {
        if (health <= 0f) Die();
        health = Mathf.Max(0f, health - amount);
    }
    
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }
    
    public void Heal(float amount)
    {
        if (health <= 0f) Die();
        health = Mathf.Min(maxHealth, health + amount);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        //Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDir, 0));
    }

    void Die()
    {
            
            gameManager.showScene();
      
    }

}
