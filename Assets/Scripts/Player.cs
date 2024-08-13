using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, IKitchenObjectParent
{
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter; 
    }
    
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float forwardRotateSpeed = 30f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldOnPoint;

    private KitchenObject _kitchenObject;
    private bool _isMoving;
    private Vector3 _lastInteractDir;
    private BaseCounter _selectedCounter;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) {
        
            Debug.LogError("There is more than 1 player");
        }

        Instance = this;
    }

    private void Start()
    {
        
        gameInput.OnInteractAction += GameInputOnOnInteractAction;
        gameInput.OnInteractAlternativeAction += GameInputOnOnInteractAlternativeAction;
    }

    private void GameInputOnOnInteractAlternativeAction(object sender, EventArgs e) {
        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlternative(this);
        }
    }

    private void GameInputOnOnInteractAction(object sender, EventArgs e)
    {
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
        
    }

    public bool IsMoving()
    {
        return _isMoving;
    }

    void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            _lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                if (clearCounter != _selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter clearCounter)
    {
        _selectedCounter = clearCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{selectedCounter = _selectedCounter});
    }
        
    void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = .7f;
        bool canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }

        }

        if (canMove)
        {
            transform.position += moveDir * (moveSpeed * Time.deltaTime);
        }

        _isMoving = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, forwardRotateSpeed * Time.deltaTime);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldOnPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {

        this._kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this._kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
