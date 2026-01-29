using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform recipesContainer;
    [SerializeField] Transform recipeTemplate;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawned += UpdateVisual;
        DeliveryManager.Instance.OnRecipeCompleted += UpdateVisual;
    }

    private void UpdateVisual() {
        foreach (Transform transform in recipesContainer) {
            if (transform == recipeTemplate) continue;
            Destroy(transform.gameObject);
        }

        foreach (RecipeSO recipe in DeliveryManager.Instance.GetWaitingRecipes()) {
            Transform recipeTransform = Instantiate(recipeTemplate, recipesContainer);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipe(recipe);
        }
    }
}
