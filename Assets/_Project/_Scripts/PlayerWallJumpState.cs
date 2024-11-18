using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(PlayerController _player, PlayerStateMachine _stateMachine, int _animBoolId) : base(_player, _stateMachine, _animBoolId) { }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 1f;
        player.SetVelocity(5 * -player.FacingDir, player.ChargedJumpForce);
    }

    public override void Exit() => base.Exit();

    public override void Update()
    {
        base.Update();

        CheckStateTimer();
        CheckForDoubleJump();
        CheckForGroundState();
        CheckForWallSlide();
        HandleMovement();
    }

    private void CheckStateTimer()
    {
        if (stateTimer < 0)
            stateMachine.ChangeState(player.AirState);
    }

    private void CheckForDoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            player.PerformDoubleJump();
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

    private void HandleMovement()
    {
        if (xInputValue != 0)
            player.SetVelocity(player.MoveSpeed * 0.8f * xInputValue, rb.linearVelocity.y);
    }
}
