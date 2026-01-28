using UnityEngine;

public class StoveCounterVisual : CounterVisual {
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private GameObject heatGameObject;

    private new void Start() {
        base.Start();
        (counter as StoveCounter).OnStateChanged += HandleStateChanged;
    }

    private void HandleStateChanged(StoveCounter.StoveState state) {
        if (state == StoveCounter.StoveState.Frying || state == StoveCounter.StoveState.Burning) {
            ShowEffects();
        } else {
            HideEffects();
        }
    }

    private void ShowEffects() {
        particlesGameObject.SetActive(true);
        heatGameObject.SetActive(true);
    }

    private void HideEffects() {
        particlesGameObject.SetActive(false);
        heatGameObject.SetActive(false);
    }
}
