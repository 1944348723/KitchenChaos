using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent parent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return this.kitchenObjectSO;
    }

    public void SetParent(IKitchenObjectParent parent) {
        if (this.parent != null) {
            this.parent.ClearKitchenObject();
        }

        this.parent = parent;
        parent.SetKitchenObject(this);
    }

    public IKitchenObjectParent GetParent() {
        return this.parent;
    }

    public void DestroySelf() {
        transform.SetParent(null);
        parent = null;
        Destroy(gameObject);
    }
}
