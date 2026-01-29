using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform ingredientsContainer;
    [SerializeField] private Transform ingredientIconTemplate;

    private void Awake() {
        ingredientIconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipe(RecipeSO recipeSO) {
        recipeNameText.text = recipeSO.recipeName;
        foreach (KitchenObjectSO ingredient in recipeSO.ingredients) {
            Transform ingredientTransform = Instantiate(ingredientIconTemplate, ingredientsContainer);
            ingredientTransform.gameObject.SetActive(true);
            ingredientTransform.GetComponent<Image>().sprite = ingredient.sprite;
        }
    }
}
