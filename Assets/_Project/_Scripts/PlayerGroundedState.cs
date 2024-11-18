using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private float jumpForceMultiplier = 10f;

    public PlayerGroundedState(PlayerController _player, PlayerStateMachine _stateMachine, int _animBoolId) : base(_player, _stateMachine, _animBoolId) { }

    public override void Enter()
    {
        base.Enter();
        player.ResetJumpForce();
    }

    public override void Exit() => base.Exit();

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Space) && player.IsGroundDetected())
            player.ChargedJumpForce += Time.deltaTime * jumpForceMultiplier;

        if (Input.GetKeyUp(KeyCode.Space))
            stateMachine.ChangeState(player.JumpState);
    }
}
