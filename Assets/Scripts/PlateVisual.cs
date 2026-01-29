using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateVisual : MonoBehaviour
{
    [Serializable]
    private struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    };

    [SerializeField] Plate plate;
    [SerializeField] KitchenObjectSO_GameObject[] kitchenObjectSO_GameObjects;

    private void Start() {
        plate.OnIngredientAdded += HandleIngredientAdded;
        foreach(var pair in kitchenObjectSO_GameObjects) {
            pair.gameObject.SetActive(false);
        }
    }

    private void HandleIngredientAdded(KitchenObjectSO kitchenObjectSO) {
        foreach (var pair in kitchenObjectSO_GameObjects) {
            if (pair.kitchenObjectSO == kitchenObjectSO) {
                pair.gameObject.SetActive(true);
                break;
            }
        }
    }
}
