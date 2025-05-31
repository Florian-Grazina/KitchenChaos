using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractActions;

    protected void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable(); // enable map

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(CallbackContext obj)
    {
        OnInteractActions?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementInputNormalized()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
    }
}
