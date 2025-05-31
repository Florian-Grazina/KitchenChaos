using Assets.Scripts.Events;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region serialize fields
    [SerializeField] GameInput gameInput;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float interactDistance = 2f;

    [SerializeField] private LayerMask countersLayerMask;
    #endregion

    #region properties
    public static Player Instance { get; private set; }
    #endregion

    #region event
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    #endregion

    #region private fields
    private bool _isWalking;
    private ClearCounter _selectedCounter;
    #endregion

    #region unity methods
    protected void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("Multiple Player instances detected. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        if (gameInput != null)
            gameInput.OnInteractActions += GameInput_HandleInteractions;
    }

    protected void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    #endregion

    #region public methods
    public bool IsWalking() => _isWalking;
    #endregion

    #region GameInput methods
    private void GameInput_HandleInteractions(object sender, EventArgs args)
    {
        if(_selectedCounter != null)
            _selectedCounter.Interact();
    }
    #endregion

    #region private methods
    private void HandleMovement()
    {
        Vector2 movementInput = gameInput.GetMovementInputNormalized();
        Vector3 moveDir = new(movementInput.x, 0, movementInput.y);

        _isWalking = moveDir != Vector3.zero;
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

    private void HandleInteractions()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            Debug.DrawLine(transform.position, raycastHit.point, Color.red);
            ClearCounter clearCounter = raycastHit.collider.GetComponent<ClearCounter>();
            SetSelecterCounter(clearCounter);
        }
        else
        {
            SetSelecterCounter(null);
            Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.green);
        }
    }

    private void SetSelecterCounter(ClearCounter newCounter)
    {
        if(_selectedCounter != newCounter)
        {
            _selectedCounter = newCounter;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs(_selectedCounter));
        }
    }
    #endregion
}
