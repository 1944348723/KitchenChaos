using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent, IInteractable {
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact(Player player) {
        if (!kitchenObject && player.HasKitchenObject()) {
            player.GetKitchenObject().SetParent(this);
        } else if (kitchenObject && !player.HasKitchenObject()) {
            kitchenObject.SetParent(player);
        }
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        if (this.kitchenObject) {
            Debug.Log("ClearCounter already has a kitchenobject");
            return;
        }

        kitchenObject.transform.parent = counterTopPoint.transform;
        kitchenObject.transform.localPosition = Vector3.zero;
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
        this.counterTopPoint.DetachChildren();
    }

    public bool HasKitchenObject() {
        return this.kitchenObject != null;
    }

    public KitchenObject GetKitchenObject() {
        return this.kitchenObject;
    }
}
