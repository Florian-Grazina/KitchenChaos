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

    protected void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
        Debug.Log($"Raw Input: {movementInput}");
    }

    private void Move()
    {
        Vector2 delta = speed * Time.deltaTime * movementInput;

        Vector3 newPos = new()
        {
            x = transform.position.x + delta.x,
            z = transform.position.z + delta.y
        };

        transform.position = newPos;
    }
}
