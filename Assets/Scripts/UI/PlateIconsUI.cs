using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private Plate plate;
    [SerializeField] private Transform iconTempalte;

    private void Start() {
        plate.OnIngredientAdded += HandleIngredientAdded;
        iconTempalte.gameObject.SetActive(false);
    }

    private void HandleIngredientAdded(KitchenObjectSO kitchenObjectSO) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in transform) {
            if (child == iconTempalte) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO ingredient in plate.GetIngredients()) {
            Transform ingredientTransform = Instantiate(iconTempalte, transform);
            ingredientTransform.gameObject.SetActive(true);
            ingredientTransform.GetComponent<SingleIconUI>().SetKitchenObjectSO(ingredient);
        }
    }
}
