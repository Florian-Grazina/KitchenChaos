using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractActions;
    public event EventHandler OnInteractAlternateActions;

    protected void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable(); // enable map

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void Interact_performed(CallbackContext obj)
    {
        OnInteractActions?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(CallbackContext obj)
    {
        OnInteractAlternateActions?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementInputNormalized()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
    }
}
