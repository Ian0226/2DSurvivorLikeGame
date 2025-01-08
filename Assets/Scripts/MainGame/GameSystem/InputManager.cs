using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.CustomTool;

public class InputManager : GameSystemBase
{
    private PlayerInputActions inputActions;

    public Vector2 MoveInput { get; private set; }

    public Vector2 LookInput { get; private set; }

    public InputManager(SurvivorLikeGame2DFacade survivorLikeGame) : base(survivorLikeGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        inputActions = new PlayerInputActions();

        inputActions.Enable();

        inputActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => MoveInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        //inputActions.Player.Look.canceled += _ => LookInput = Vector2.zero;

        inputActions.Player.Attack.performed += ctx => OnClick();
        inputActions.Player.Attack.canceled += ctx => OnRelease();
        //inputActions.Player.Attack.canceled += ctx => IsAttack = false;
    }

    public void OnClick()
    {
        //survivorLikeGame.HandleAttack();
        survivorLikeGame.SetPlayerAttackState(true);
    }

    public void OnRelease()
    {
        survivorLikeGame.SetPlayerAttackState(false);
    }

    public void EnableInputManager()
    {
        inputActions.Enable();
    }

    public override void Release()
    {
        inputActions.Disable();
    }
}
