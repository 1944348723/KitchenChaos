using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenObject {
    [SerializeField] private List<KitchenObjectSO> validIngredients;

    private List<KitchenObjectSO> ingredients = new();

    public bool TryAddIngredient(KitchenObject kitchenObject) {
        KitchenObjectSO kitchenObjectSO = kitchenObject.GetKitchenObjectSO();
        if (validIngredients.Contains(kitchenObjectSO) && !ingredients.Contains(kitchenObjectSO)) {
            ingredients.Add(kitchenObjectSO);
            return true;
        } else {
            return false;
        }
    }
}
