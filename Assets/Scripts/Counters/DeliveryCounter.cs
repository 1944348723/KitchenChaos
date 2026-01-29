using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour, IInteractable {
    public void Interact(Player player) {
        if (player.GetKitchenObject() is Plate) {
            Plate plate = player.GetKitchenObject() as Plate;
            DeliveryManager.Instance.Deliver(plate.GetIngredients());
            plate.DestroySelf();
        }
    }
}
