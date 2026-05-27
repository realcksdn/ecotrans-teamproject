using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Movement movement;

    private PlayerAction action;
    public PlayerAction.OnFootActions onFoot;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        action = new PlayerAction();
        onFoot = action.onFoot;

        action.Enable();
    }

    private void FixedUpdate()
    {
        movement.ProcessMove(onFoot.Move.ReadValue<Vector2>());
    }
}
