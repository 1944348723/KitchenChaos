using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenObject {
    [SerializeField] private List<KitchenObjectSO> validIngredients;

    public event System.Action<KitchenObjectSO> OnIngredientAdded;

    private List<KitchenObjectSO> ingredients = new();

    public bool TryAddIngredient(KitchenObject kitchenObject) {
        KitchenObjectSO kitchenObjectSO = kitchenObject.GetKitchenObjectSO();
        if (validIngredients.Contains(kitchenObjectSO) && !ingredients.Contains(kitchenObjectSO)) {
            ingredients.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(kitchenObjectSO);
            return true;
        } else {
            return false;
        }
    }

    public List<KitchenObjectSO> GetIngredients() {
        return ingredients;
    }
}
