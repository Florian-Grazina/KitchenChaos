using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Vector2 movementInput;

    protected void Update()
    {
        Move();
    }

    protected void OnMovement(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
        Debug.Log($"Raw Input: {movementInput}");

    }

    private void Move()
    {
        Vector2 delta = speed * Time.deltaTime * movementInput;

        Vector2 newPos = new()
        {
            x = transform.position.x + delta.x,
            y = transform.position.y + delta.y
        };

        transform.position = newPos;
    }
}
