using Assets.Scripts.Events;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectHolder
{
    #region serialize fields
    [SerializeField] GameInput gameInput;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float interactDistance = 2f;

    [SerializeField] private LayerMask countersLayerMask;

    [SerializeField] private Transform kitchenObjectHoldPoint;
    #endregion

    #region private fields
    private KitchenObject _kitchenObject;
    private bool _isWalking;
    private BaseCounter _selectedCounter;
    #endregion

    #region properties
    public static Player Instance { get; private set; }
    #endregion

    #region event
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
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
        {
            gameInput.OnInteractActions += GameInput_HandleInteractions;
            gameInput.OnInteractAlternateActions += GameInput_HandleAlternateInteractions;
        }
    }

    protected void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (_kitchenObject != null)
            {
                Destroy(_kitchenObject.gameObject);
                ClearKitchenObject();
            }
        }

        HandleMovement();
        HandleCounterSelection();
    }
    #endregion

    #region public methods
    public bool IsWalking() => _isWalking;
    #endregion

    #region IKitchenObjectHolder
    public void SetKitchenObject(KitchenObject kitchenObject) => _kitchenObject = kitchenObject;

    public KitchenObject GetKitchenObject() => _kitchenObject;

    public void ClearKitchenObject() => _kitchenObject = null;

    public bool HasKitchenObject() => _kitchenObject != null;

    public Transform GetKitchenObjectFollowTransform() => kitchenObjectHoldPoint;
    #endregion

    #region GameInput methods
    private void GameInput_HandleInteractions(object sender, EventArgs args)
    {
        if(_selectedCounter != null)
            _selectedCounter.Interact(this);
    }

    private void GameInput_HandleAlternateInteractions()
    {
        if (_selectedCounter != null)
            _selectedCounter.InteractAlternate(this);
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

    private void HandleCounterSelection()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            Debug.DrawLine(transform.position, raycastHit.point, Color.red);
            BaseCounter baseCounter = raycastHit.collider.GetComponent<BaseCounter>();
            SetSelecterCounter(baseCounter);
        }
        else
        {
            SetSelecterCounter(null);
            Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.green);
        }
    }

    private void SetSelecterCounter(BaseCounter newCounter)
    {
        if(_selectedCounter != newCounter)
        {
            _selectedCounter = newCounter;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs(_selectedCounter));
        }
    }
    #endregion
}
