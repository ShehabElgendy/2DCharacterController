using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerController _player, PlayerStateMachine _stateMachine, int _animBoolId) : base(_player, _stateMachine, _animBoolId) { }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(0, 0);
    }

    public override void Exit() => base.Exit();

    public override void Update()
    {
        base.Update();

        if (xInputValue == player.FacingDir && player.IsWallDetected())
            return;

        if (xInputValue != 0)
            stateMachine.ChangeState(player.MoveState);
    }
}
