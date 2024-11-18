using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStatics
{
    public static readonly int IDLE_ANIMATION_PARAMETER = Animator.StringToHash("Idle");
    public static readonly int MOVE_ANIMATION_PARAMETER = Animator.StringToHash("Move");
    public static readonly int JUMP_ANIMATION_PARAMETER = Animator.StringToHash("Jump");
    public static readonly int SLIDE_ANIMATION_PARAMETER = Animator.StringToHash("Dash");
    public static readonly int WALLSLIDE_ANIMATION_PARAMETER = Animator.StringToHash("WallSlide");

    public static readonly string HORIZONTAL_AXIS_PARAMETER = "Horizontal";
    public static readonly string VERTICAL_AXIS_PARAMETER = "Vertical";
    public static readonly string YVELOCITY_AXIS_PARAMETER = "yVelocity";

}
