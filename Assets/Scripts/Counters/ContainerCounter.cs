using UnityEngine;

public class ContainerCounter : MonoBehaviour, IInteractable {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event System.Action OnKitchenObjectSpawned;

    public void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetParent(player);
            OnKitchenObjectSpawned?.Invoke();
        } 
    }
}
