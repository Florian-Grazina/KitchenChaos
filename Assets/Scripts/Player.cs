using UnityEngine;

public class Player : MonoBehaviour
{
    #region serialize fields
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float interactDistance = 2f;

    [SerializeField] private LayerMask countersLayerMask;

    [SerializeField] GameInput gameInput;
    #endregion

    #region private fields
    private bool isWalking;
    private 
    #endregion

    #region unity methods
    protected void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    #endregion

    #region public methods
    public bool IsWalking() => isWalking;
    #endregion

    #region private methods
    private void HandleInteractions()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            Debug.DrawLine(transform.position, raycastHit.point, Color.red);

            if(raycastHit.collider.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.green);
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
    #endregion
}
