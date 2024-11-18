using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerController _player, PlayerStateMachine _stateMachine, int _animBoolId) : base(_player, _stateMachine, _animBoolId) { }

    public override void Enter() => base.Enter();

    public override void Exit() => base.Exit();

    public override void Update()
    {
        base.Update();

        if (CheckForWallJump()) return;

        CheckForDirectionChange();
        HandleWallSlideMovement();
        CheckForGroundState();
    }

    private bool CheckForWallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.WallJump);
            return true;
        }
        return false;
    }

    private void CheckForDirectionChange()
    {
        if (xInputValue != 0 && player.FacingDir != xInputValue)
            stateMachine.ChangeState(player.IdleState);
    }

    private void HandleWallSlideMovement()
    {
        float slideVelocity = yInputValue < 0
            ? rb.linearVelocity.y
            : rb.linearVelocity.y * .7f;

        rb.linearVelocity = new Vector2(0, slideVelocity);
    }

    private void CheckForGroundState()
    {
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }
}
