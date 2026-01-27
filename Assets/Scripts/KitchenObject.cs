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

    public static KitchenObject Spawn(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetParent(kitchenObjectParent);
        return kitchenObject;
    }

    public void DestroySelf() {
        if (parent != null) {
            parent.ClearKitchenObject();
        }
        parent = null;
        Destroy(gameObject);
    }
}
