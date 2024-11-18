using UnityEngine;

public class PlayerSlideState : PlayerState
{
    public PlayerSlideState(PlayerController _player, PlayerStateMachine _stateMachine, int _animBoolId) : base(_player, _stateMachine, _animBoolId) { }

    public override void Enter()
    {
        base.Enter();
        SetColliderSize();
        stateTimer = player.SlideDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.linearVelocity.y);
        ResetColliderSize();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.SlideSpeed * player.SlideDir, 0);

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.WallSlide);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.IdleState);
    }
    private void SetColliderSize()
    {
        Vector2 currentSize = player.CapsuleCollider.size;
        currentSize.y = 1;
        player.CapsuleCollider.size = currentSize;
        Vector2 currentOffset = player.CapsuleCollider.offset;
        currentOffset.y = -0.8f;
        player.CapsuleCollider.offset = currentOffset;
    }
    private void ResetColliderSize()
    {
        Vector2 currentSize = player.CapsuleCollider.size;
        currentSize.y = 2;
        player.CapsuleCollider.size = currentSize;
        Vector2 currentOffset = player.CapsuleCollider.offset;
        currentOffset.y = -0.33f;
        player.CapsuleCollider.offset = currentOffset;
    }
}
