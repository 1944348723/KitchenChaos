using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent, IInteractable {
    [SerializeField] private Transform counterTopPoint;

    public static event System.Action<Vector3> OnObjectPlacedOn;
    private KitchenObject kitchenObject;

    public void Interact(Player player) {
        if (!kitchenObject && player.HasKitchenObject()) {
            player.GetKitchenObject().SetParent(this);
        } else if (kitchenObject && !player.HasKitchenObject()) {
            kitchenObject.SetParent(player);
        } else if (kitchenObject && player.HasKitchenObject()) {
            // 橱柜上的是盘子
            if (kitchenObject is Plate) {
                Plate plate = kitchenObject as Plate;
                if (plate.TryAddIngredient(player.GetKitchenObject())) {
                    player.GetKitchenObject().DestroySelf();
                }
            // 玩家手里的是盘子
            } else if (player.GetKitchenObject() is Plate) {
                Plate plate = player.GetKitchenObject() as Plate;
                if (plate.TryAddIngredient(kitchenObject)) {
                    kitchenObject.DestroySelf();
                }
            }
        }
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        if (this.kitchenObject) {
            Debug.Log("ClearCounter already has a kitchenobject");
            return;
        }
        if (!kitchenObject) return;

        kitchenObject.transform.parent = counterTopPoint.transform;
        kitchenObject.transform.localPosition = Vector3.zero;
        this.kitchenObject = kitchenObject;
        OnObjectPlacedOn?.Invoke(transform.position);
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
