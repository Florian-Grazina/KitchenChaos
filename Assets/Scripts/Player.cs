using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float interactDistance = 2f;


    [SerializeField] GameInput gameInput;

    private bool isWalking;
    public bool IsWalking() => isWalking;

    protected void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 movementInput = gameInput.GetMovementInputNormalized();
        Vector3 moveDir = new(movementInput.x, 0, movementInput.y);

        if (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance))
        {
            Debug.Log($"Interacting with {raycastHit.collider.name}");
        }
    }

    private void HandleMovement()
    {
        Vector2 movementInput = gameInput.GetMovementInputNormalized();
        Vector3 moveDir = new(movementInput.x, 0, movementInput.y);

        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

        float playerHeight = 2f;
        float playerRadius = 0.5f;
        float moveDistance = Time.deltaTime * moveSpeed;

        Vector3 capsuleBase = transform.position;
        Vector3 capsuleTop = capsuleBase + Vector3.up * playerHeight;

        // can move X
        if (Physics.CapsuleCast(capsuleBase, capsuleTop, playerRadius, new(moveDir.x, 0, 0), moveDistance))
            moveDir.x = 0;

        // can move Z
        if (Physics.CapsuleCast(capsuleBase, capsuleTop, playerRadius, new(0, 0, moveDir.z), moveDistance))
            moveDir.z = 0;

        transform.position += moveDistance * moveDir.normalized;
    }
}
