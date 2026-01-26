using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    public static Player Instance { get; private set; }
    public event System.Action<IInteractable> OnSelectedObjectChanged;

    private const float PLAYER_RADIUS = 0.7f;
    private const float PLAYER_HEIGHT = 1;
    private const float INTERACT_DISTANCE = 2f;
    private bool isWalking = false;
    private IInteractable selectedObject;
    private KitchenObject kitchenObject;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Debug.LogWarning("Player Instance Recreated");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAcion += PerformInteract;
    }

    private void Update() {
        HandleMovement();
        UpdateInteractableObject();
    }

    public bool IsWalking() {
        return this.isWalking;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        if (this.kitchenObject) {
            Debug.Log("Player already has a kitchenobject");
            return;
        }

        kitchenObject.transform.parent = kitchenObjectHoldPoint.transform;
        kitchenObject.transform.localPosition = Vector3.zero;
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
        this.kitchenObjectHoldPoint.DetachChildren();
    }

    public bool HasKitchenObject() {
        return this.kitchenObject != null;
    }

    public KitchenObject GetKitchenObject() {
        return this.kitchenObject;
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new(inputVector.x, 0, inputVector.y);
        this.isWalking = !moveDir.Equals(Vector3.zero);

        // 转向
        this.transform.forward = Vector3.Slerp(this.transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        float movement = Time.deltaTime * moveSpeed;
        Vector3 capsuleBottom = transform.position;
        Vector3 capsuleTop = transform.position + Vector3.up * PLAYER_HEIGHT;

        // 朝向方向
        bool canMove = !Physics.CapsuleCast(capsuleBottom, capsuleTop, PLAYER_RADIUS, moveDir, movement);
        
        // X方向
        if (!canMove && moveDir.x != 0f) {
            Vector3 moveDirX = new(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(capsuleBottom, capsuleTop, PLAYER_RADIUS, moveDirX, movement * Math.Abs(moveDir.x));
            if (canMove) {
                moveDir = moveDirX;
            }
        }

        // Z方向
        if (!canMove && moveDir.z != 0f) {
            Vector3 moveDirZ = new(0, 0, moveDir.z);
            canMove = !Physics.CapsuleCast(capsuleBottom, capsuleTop, PLAYER_RADIUS, moveDirZ, movement * Math.Abs(moveDir.z));
            if (canMove) {
                moveDir = moveDirZ;
            }
        }

        if (canMove) {
            this.transform.position += moveDir * movement;
        } 
    }

    private void UpdateInteractableObject() {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, INTERACT_DISTANCE, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent<IInteractable>(out IInteractable interactable)) {
                SetSelectedObject(interactable);
            } else {
                SetSelectedObject(null);
            }
        } else {
            SetSelectedObject(null);
        }
    }

    private void PerformInteract() {
        selectedObject?.Interact(this);
    }

    private void SetSelectedObject(IInteractable obj) {
        if (selectedObject != obj) {
            selectedObject = obj;
            OnSelectedObjectChanged?.Invoke(selectedObject);
        }
    }
}
