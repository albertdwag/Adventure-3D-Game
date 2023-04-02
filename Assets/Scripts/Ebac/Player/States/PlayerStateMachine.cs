using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Ebac.StateMachine;

public class PlayerStateMachine : Singleton<PlayerStateMachine>
{
    public enum PlayerStates
    {
        IDLE,
        MOVING,
        JUMPING
    }

    public StateMachine<PlayerStates> stateMachine;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.Init();
        stateMachine.RegisterStates(PlayerStates.IDLE, new PlayerIdleState());
        stateMachine.RegisterStates(PlayerStates.MOVING, new PlayerMovingState());
        stateMachine.RegisterStates(PlayerStates.JUMPING, new PlayerJumpingState());

        stateMachine.SwitchState(PlayerStates.IDLE);
    }
}
