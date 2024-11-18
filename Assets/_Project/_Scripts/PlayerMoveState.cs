public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerController _player, PlayerStateMachine _stateMachine, int _animBoolId) : base(_player, _stateMachine, _animBoolId) { }

    public override void Enter() => base.Enter();

    public override void Exit() => base.Exit();

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.MoveSpeed * xInputValue, rb.linearVelocity.y);

        if (xInputValue == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.IdleState);
    }
}
