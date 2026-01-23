using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event System.Action OnInteractAcion;
    private PlayerInputActions playerInputActions;

    private void Awake() {
        this.playerInputActions = new PlayerInputActions();
        this.playerInputActions.Player.Enable();
        this.playerInputActions.Player.Interact.performed += InteractPerformed;
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = this.playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector.Normalize();
        return inputVector;
    }

    private void InteractPerformed(InputAction.CallbackContext context) {
        OnInteractAcion?.Invoke();
    }
}
