﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using static AnimationParamString;

public class Hero : DamageableActor
{
    public HeroData heroData;

    public Movement movement;

    public StateMachine stateMachine;

    public CollisionChecker collisionChecker;

    public WeaponSword weaponSword;

    #region state

    public HeroIdleState IdleState;
    public HeroRunState RunState;
    public HeroInAirState InAirState;
    public HeroJumpState JumpState;
    public HeroDashState DashState;
    public HeroAttackState AttackState;
    public HeroLandState LandState;
    public HeroWallClimbState WallClimbState;
    public HeroWallGrabState WallGrabState;
    public HeroWallSlideState WallSlideState;
    public HeroWallJumpState WallJumpState;
    public HeroLedgeClimbState LedgeClimbState;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        movement = GetComponentInChildren<Movement>();
        collisionChecker = GetComponentInChildren<CollisionChecker>();
        InitStateMachine();

        Assert.IsNotNull(heroData, "heroData is Null, please check your setting");
        Assert.IsNotNull(weaponSword, "weaponSword is Null, please check your setting");
    }

    private void Start()
    {
        movement.SetGravityScale(heroData.defaultGravityScale);
    }

    private void InitStateMachine()
    {
        stateMachine = new StateMachine();
        IdleState = new HeroIdleState(stateMachine, this, BOOL_IDLE);
        RunState = new HeroRunState(stateMachine, this, BOOL_RUN);
        InAirState = new HeroInAirState(stateMachine, this, BOOL_INAIR);
        JumpState = new HeroJumpState(stateMachine, this, BOOL_INAIR);
        DashState = new HeroDashState(stateMachine, this, BOOL_DASH);
        AttackState = new HeroAttackState(stateMachine, this, BOOL_ATTACK);
        AttackState.SetWeapon(weaponSword);
        LandState = new HeroLandState(stateMachine, this, BOOL_LAND);
        WallClimbState = new HeroWallClimbState(stateMachine, this, BOOL_WALL_CLIMB);
        WallGrabState = new HeroWallGrabState(stateMachine, this, BOOL_WALL_GRAB);
        WallSlideState = new HeroWallSlideState(stateMachine, this, BOOL_WALL_SLIDE);
        WallJumpState = new HeroWallJumpState(stateMachine, this, BOOL_INAIR);
        LedgeClimbState = new HeroLedgeClimbState(stateMachine, this, BOOL_LEDGE_HANG);

        stateMachine.Init(IdleState);
    }

    private void Update()
    {
        movement.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }


    public virtual void AnimationTrigger() {
        stateMachine.currentState.AnimationTrigger();
    }

    public virtual void AnimationFinishTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }
}
