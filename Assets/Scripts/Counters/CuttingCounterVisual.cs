using UnityEngine;

public class CuttingCounterVisual : CounterVisual {
    private Animator animator;
    private const string CUT = "Cut";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private new void Start() {
        base.Start();
        (counter as CuttingCounter).OnCut += HandleCut;
    }

    private void HandleCut() {
        animator.SetTrigger(CUT);
    }
}
