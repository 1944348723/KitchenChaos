using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] CuttingCounter CuttingCounter;
    [SerializeField] GameObject selectedVisual;

    private Animator animator;
    private const string CUT = "Cut";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        Player.Instance.OnSelectedObjectChanged += HandleSelectedObjectChanged;
        CuttingCounter.OnCut += HandleCut;
    }

    private void HandleSelectedObjectChanged(IInteractable newSelectedObject) {
        if (newSelectedObject == (this.CuttingCounter as IInteractable)) {
            EnableHighlight();
        } else {
            DisableHighlight();
        }
    }

    private void HandleCut() {
        animator.SetTrigger(CUT);
    }

    private void EnableHighlight() {
        selectedVisual.SetActive(true);
    }

    private void DisableHighlight() {
        selectedVisual.SetActive(false);
    }
}
