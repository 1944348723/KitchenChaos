using UnityEngine;

public class TrashCounter : MonoBehaviour, IInteractable
{
    public void Interact(Player player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
