using UnityEngine;

public class TrashCounter : MonoBehaviour, IInteractable
{
    public static event System.Action<Vector3> OnTrash;
    public void Interact(Player player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
            OnTrash?.Invoke(transform.position);
        }
    }
}
