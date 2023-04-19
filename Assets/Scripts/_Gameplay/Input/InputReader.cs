using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using static Controls;

[CreateAssetMenu(fileName = "InputReader", menuName = "AnimeMV-Testing/InputReader", order = 0)]
public class InputReader : ScriptableObject, IPlayerActions
{
    public Vector2 MovementValue { get; private set; }

    private Controls controls;

    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
    }

    public void OnLook(InputAction.CallbackContext context) {}

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }
}
