using UnityEngine;

public class ContainerCounter : MonoBehaviour, IInteractable {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event System.Action OnKitchenObjectSpawned;

    public void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            KitchenObject.Spawn(kitchenObjectSO, player);
            OnKitchenObjectSpawned?.Invoke();
        } 
    }
}
