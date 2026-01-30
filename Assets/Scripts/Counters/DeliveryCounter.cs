using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour, IInteractable {
    public static event System.Action<Vector3> OnDeliverySuccess;
    public static event System.Action<Vector3> OnDeliveryFailed;
    public void Interact(Player player) {
        if (player.GetKitchenObject() is Plate) {
            Plate plate = player.GetKitchenObject() as Plate;
            bool success = DeliveryManager.Instance.Deliver(plate.GetIngredients());
            if (success) {
                OnDeliverySuccess?.Invoke(transform.position);
            } else {
                OnDeliveryFailed?.Invoke(transform.position);
            }
            plate.DestroySelf();
        }
    }
}
