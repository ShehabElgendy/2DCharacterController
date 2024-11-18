using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController player;
    protected Rigidbody2D rb;
    
    protected float xInputValue;
    protected float yInputValue;
    private int animBoolId;

    protected float stateTimer;

    public PlayerState(PlayerController _player, PlayerStateMachine _stateMachine, int _animBoolId)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolId = _animBoolId;
    }

    public virtual void Enter()
    {
        player.Anim.SetBool(animBoolId, true);
        rb = player.Rb;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInputValue = Input.GetAxisRaw(GameStatics.HORIZONTAL_AXIS_PARAMETER);
        yInputValue = Input.GetAxisRaw(GameStatics.VERTICAL_AXIS_PARAMETER);
        player.Anim.SetFloat(GameStatics.YVELOCITY_AXIS_PARAMETER, rb.linearVelocity.y);
    }

    public virtual void Exit() => player.Anim.SetBool(animBoolId, false);

}
