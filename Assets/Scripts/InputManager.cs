using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public Vector2 MoveInput { get; private set; }

    public Vector2 LookInput { get; private set; }

    void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += _ => MoveInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        //inputActions.Player.Look.canceled += _ => LookInput = Vector2.zero;
    }

    void OnEnable() => inputActions.Enable();
    void OnDisable() => inputActions.Disable();
}
