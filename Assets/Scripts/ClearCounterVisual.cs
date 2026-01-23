using UnityEngine;

public class ClearCounterVisual : MonoBehaviour {
    [SerializeField] ClearCounter clearCounter;
    [SerializeField] GameObject selectedVisual;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += HandleSelectedCounterChanged;
    }

    private void HandleSelectedCounterChanged(ClearCounter newSelectedCounter) {
        if (newSelectedCounter == this.clearCounter) {
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
