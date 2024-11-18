using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Locomotion")]
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float defaultJumpForce = 12f;
    [SerializeField] private float doubleJumpForce = 10f;
    [SerializeField] private bool hasDoubleJumpAbility = true;
    private float chargedJumpForce = 12f;
    private bool canDoubleJump;

    [Header("Sliding")]
    [SerializeField] private float slideCooldown;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideDuration;
    private float slideTimer;
    private float slideDir;

    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool facingRight = true;
    private int facingDir = 1;
    #endregion

    #region Properties
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    public float ChargedJumpForce { get => chargedJumpForce; set => chargedJumpForce = value; }
    public float SlideSpeed { get => slideSpeed; set => slideSpeed = value; }
    public float SlideDuration { get => slideDuration; set => slideDuration = value; }
    public float SlideDir { get => slideDir; set => slideDir = value; }
    public int FacingDir { get => facingDir; set => facingDir = value; }
    #endregion

    #region Components
    private Animator anim;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    public Animator Anim { get => anim; private set => anim = value; }
    public Rigidbody2D Rb { get => rb; private set => rb = value; }
    public CapsuleCollider2D CapsuleCollider { get => capsuleCollider; private set => capsuleCollider = value; }
    #endregion

    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerWallSlideState WallSlide { get; private set; }
    public PlayerWallJumpState WallJump { get; private set; }
    public PlayerSlideState SlideState { get; private set; }
    #endregion

    #region Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, GameStatics.IDLE_ANIMATION_PARAMETER);
        MoveState = new PlayerMoveState(this, StateMachine, GameStatics.MOVE_ANIMATION_PARAMETER);
        JumpState = new PlayerJumpState(this, StateMachine, GameStatics.JUMP_ANIMATION_PARAMETER);
        AirState = new PlayerAirState(this, StateMachine, GameStatics.JUMP_ANIMATION_PARAMETER);
        SlideState = new PlayerSlideState(this, StateMachine, GameStatics.SLIDE_ANIMATION_PARAMETER);
        WallSlide = new PlayerWallSlideState(this, StateMachine, GameStatics.WALLSLIDE_ANIMATION_PARAMETER);
        WallJump = new PlayerWallJumpState(this, StateMachine, GameStatics.JUMP_ANIMATION_PARAMETER);
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        StateMachine.Initialize(IdleState);
        ResetDoubleJump();
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
        CheckForSlideInput();

        if (IsGroundDetected())
            ResetDoubleJump();
    }

    public void PerformDoubleJump()
    {
        if (hasDoubleJumpAbility && canDoubleJump)
        {
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, doubleJumpForce);
            canDoubleJump = false;
        }
    }
    private void CheckForSlideInput()
    {
        if (IsWallDetected() || Rb.linearVelocity.y != 0) return;

        slideTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && slideTimer <= 0)
        {
            slideTimer = slideCooldown;
            slideDir = Input.GetAxisRaw(GameStatics.HORIZONTAL_AXIS_PARAMETER);

            if (SlideDir == 0)
                SlideDir = facingDir;

            StateMachine.ChangeState(SlideState);
        }
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        Rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
    public void ResetJumpForce() => ChargedJumpForce = defaultJumpForce;
    public void ResetDoubleJump() => canDoubleJump = hasDoubleJumpAbility;
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDir, wallCheckDistance, whatIsGround);
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    #endregion
}