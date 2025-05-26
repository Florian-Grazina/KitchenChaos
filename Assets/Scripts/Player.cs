using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationSpeed = 10f;

    private bool isWalking;
    private Vector2 movementInput;

    protected void Update()
    {
        Move();
    }

    protected void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>().normalized;
    }

    public bool IsWalking() => isWalking;

    private void Move()
    {
        Vector3 moveDir = new(movementInput.x, 0f, movementInput.y);
        transform.position += speed * Time.deltaTime * moveDir;

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }
}
