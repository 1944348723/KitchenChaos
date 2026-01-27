using UnityEngine;

public class ContainerCounterVisual : CounterVisual {
    private Animator animator;
    private string OPEN_CLOSE = "OpenClose";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private new void Start() {
        base.Start();
        (counter as ContainerCounter).OnKitchenObjectSpawned += HandleKitchenObjectSpawned;
    }

    private void HandleKitchenObjectSpawned() {
        this.animator.SetTrigger(OPEN_CLOSE);
    }
}
