using UnityEngine;

public class CounterVisual : MonoBehaviour
{
    [SerializeField] protected MonoBehaviour counter;
    [SerializeField] private GameObject selectedVisual;

    protected void Start() {
        Player.Instance.OnSelectedObjectChanged += HandleSelectedObjectChanged;
    }

    private void HandleSelectedObjectChanged(IInteractable newSelectedObject) {
        if (newSelectedObject == (counter as IInteractable)) {
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
