public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerController _player, PlayerStateMachine _stateMachine, int _animBoolId) : base(_player, _stateMachine, _animBoolId) { }

    public override void Enter() => base.Enter();

    public override void Exit()
    {
        base.Exit();
        player.ResetJumpForce();
    }

    public override void Update()
    {
        base.Update();

        CheckForGroundState();
        CheckForWallSlide();
        HandleAirMovement();
    }

    private void CheckForGroundState()
    {
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }

    private void CheckForWallSlide()
    {
        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.WallSlide);
    }

    private void HandleAirMovement()
    {
        if (xInputValue != 0)
            player.SetVelocity(player.MoveSpeed * 0.8f * xInputValue, rb.linearVelocity.y);
    }
}
