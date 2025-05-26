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
        movementInput = inputValue.Get<Vector2>().normalized;
    }

    private void Move()
    {
        Vector3 newPos3 = new(movementInput.x, 0f, movementInput.y);
        transform.position += speed * Time.deltaTime * newPos3;
    }
}
