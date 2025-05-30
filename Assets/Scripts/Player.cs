using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] GameInput gameInput;

    private bool isWalking;

    protected void Update()
    {
        Move();
    }

    public bool IsWalking() => isWalking;

    private void Move()
    {
        Vector2 movementInput = gameInput.GetMovementInputNormalized();
        Vector3 moveDir = new(movementInput.x, 0f, movementInput.y);

        bool canMove = !Physics.Raycast(transform.position, transform.forward, 1);
        if(canMove)
            transform.position += speed * Time.deltaTime * moveDir;

        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }
}
