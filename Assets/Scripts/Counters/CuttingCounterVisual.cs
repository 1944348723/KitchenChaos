using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] CuttingCounter CuttingCounter;
    [SerializeField] GameObject selectedVisual;

    private void Start() {
        Player.Instance.OnSelectedObjectChanged += HandleSelectedObjectChanged;
    }

    private void HandleSelectedObjectChanged(IInteractable newSelectedObject) {
        if (newSelectedObject == (this.CuttingCounter as IInteractable)) {
            EnableHighlight();
        } else {
            DisableHighlight();
        }
    }

    private void EnableHighlight() {
        selectedVisual.SetActive(true);
    }

    private void DisableHighlight() {
        selectedVisual.SetActive(false);
    }
}
