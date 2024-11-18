using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerController player, PlayerStateMachine stateMachine, int animBoolId) : base(player, stateMachine, animBoolId) { }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.ChargedJumpForce);
    }

    public override void Exit() => base.Exit();

    public override void Update()
    {
        base.Update();

        CheckForDoubleJump();
        CheckForFalling();
        HandleAirMovement();
    }

    private void CheckForDoubleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            player.PerformDoubleJump();
    }

    private void CheckForFalling()
    {
        if (rb.linearVelocity.y < 0)
            stateMachine.ChangeState(player.AirState);
    }

    private void HandleAirMovement()
    {
        if (xInputValue != 0)
            player.SetVelocity(player.MoveSpeed * 0.8f * xInputValue, rb.linearVelocity.y);
    }
}