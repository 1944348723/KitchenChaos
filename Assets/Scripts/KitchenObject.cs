using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() {
        return this.kitchenObjectSO;
    }

    public void SetCounter(ClearCounter clearCounter) {
        this.clearCounter = clearCounter;
    }

    public ClearCounter GetCounter() {
        return this.clearCounter;
    }
}
