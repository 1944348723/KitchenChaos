using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour {
    [SerializeField] private ContainerCounter containerCounter;
    [SerializeField] private GameObject selectedVisual;

    private Animator animator;
    private string OPEN_CLOSE = "OpenClose";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        Player.Instance.OnSelectedObjectChanged += HandleSelectedObjectChanged;
        containerCounter.OnKitchenObjectSpawned += HandleKitchenObjectSpawned;
    }

    private void HandleSelectedObjectChanged(IInteractable newSelectedObject) {
        if (newSelectedObject == (this.containerCounter as IInteractable)) {
            EnableHighlight();
        } else {
            DisableHighlight();
        }
    }

    private void HandleKitchenObjectSpawned() {
        this.animator.SetTrigger(OPEN_CLOSE);
    }

    private void EnableHighlight() {
        selectedVisual.SetActive(true);
    }

    private void DisableHighlight() {
        selectedVisual.SetActive(false);
    }
}
