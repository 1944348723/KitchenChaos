using UnityEngine;

public class CuttingCounter : MonoBehaviour, IKitchenObjectParent, IInteractable
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO slices;
    private KitchenObject kitchenObject;

    public void Interact(Player player) {
        if (!kitchenObject && player.HasKitchenObject()) {
            player.GetKitchenObject().SetParent(this);
        } else if (kitchenObject && !player.HasKitchenObject()) {
            kitchenObject.SetParent(player);
        }
    }

    public void InteractAlternate(Player player) {
        if (kitchenObject) {
            kitchenObject.DestroySelf();
            kitchenObject = null;
        }
        var kitchenObjectTransform = Instantiate(slices.Prefab);
        SetKitchenObject(kitchenObjectTransform.GetComponent<KitchenObject>());
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject) {
        if (this.kitchenObject) {
            Debug.Log("CuttingCounter already has a kitchenobject");
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
