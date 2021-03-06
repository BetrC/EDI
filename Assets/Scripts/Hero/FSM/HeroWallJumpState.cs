using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWallJumpState : HeroAbilityState
{

    public int JumpDirection = 1;

    public bool JumpInputBuffer = false;

    public HeroWallJumpState(StateMachine stateMachine, Hero hero, string animatorBoolParam) : base(stateMachine, hero, animatorBoolParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        DetermineWallJumpDirection();
        hero.JumpState.ResetJumpCount();
        hero.movement.CheckFlip(JumpDirection);
        hero.movement.SetVelocity(heroData.wallJumpSpeed, heroData.wallJumpDir, JumpDirection);
        hero.JumpState.DecreaseJumpTime();
        hero.movement.SetBetterJumpEnable(false);
        hero.JumpState.ShowJumpParticle();
    }

    public override void Exit()
    {
        base.Exit();
        hero.movement.SetBetterJumpEnable(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isExitingState)
            return;

        if (Time.time > heroData.wallJumpTime + enterTime)
        {
            isAbilityDone = true;
            return;
        }

        if (hero.JumpState.TriggeredAbility())
        {
            JumpInputBuffer = true;
        }
    }

    public void DetermineWallJumpDirection()
    {
        if (hero.WallGrabState.IsOnWall())
        {
            JumpDirection = FacingDirection * -1;
        }
        else
        {
            JumpDirection = FacingDirection;
        }
    }

    public void ResetJumpInputBuffer()
    {
        if (JumpInputBuffer)
            JumpInputBuffer = false;
    }

    public override bool TriggeredAbility()
    {
        return InputManager.Instance.JumpPressed;
    }
}