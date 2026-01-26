using UnityEngine;

public class ClearCounterVisual : MonoBehaviour {
    [SerializeField] ClearCounter clearCounter;
    [SerializeField] GameObject selectedVisual;

    private void Start() {
        Player.Instance.OnSelectedObjectChanged += HandleSelectedObjectChanged;
    }

    private void HandleSelectedObjectChanged(IInteractable newSelectedObject) {
        if (newSelectedObject == (this.clearCounter as IInteractable)) {
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
