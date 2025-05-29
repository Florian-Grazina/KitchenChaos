using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    protected void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable(); // enable map
    }

    public Vector2 GetMovementInputNormalized()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
    }
}
