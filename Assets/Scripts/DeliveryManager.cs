using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {
    [SerializeField] private List<RecipeSO> allRecipes;

    public static DeliveryManager Instance { get; private set; }
    private const int MAX_WAITING_RECIPES_COUNT = 4;
    private const float RECIPE_SPAWN_INTERVAL = 4f;

    private List<RecipeSO> waitingRecipes = new();

    private float spawnTimer = 0f;
    // 缓存每个菜单内对应的食物，提交时就可以快速查找
    private Dictionary<string, HashSet<KitchenObjectSO>> ingredientsSetForRecipes = new();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Debug.LogWarning("DeliveryManager Instance Recreated");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        foreach (RecipeSO recipe in allRecipes) {
            HashSet<KitchenObjectSO> set = new();
            foreach (KitchenObjectSO ingredient in recipe.ingredients) {
                set.Add(ingredient);
            }
            ingredientsSetForRecipes[recipe.recipeName] = set;
        }
    }

    private void Update() {
        if (waitingRecipes.Count >= MAX_WAITING_RECIPES_COUNT) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= RECIPE_SPAWN_INTERVAL) {
            spawnTimer -= RECIPE_SPAWN_INTERVAL;
            RecipeSO recipe = allRecipes[Random.Range(0, allRecipes.Count - 1)];
            waitingRecipes.Add(recipe);
            Debug.Log(recipe.recipeName);

            if (waitingRecipes.Count == MAX_WAITING_RECIPES_COUNT) {
                spawnTimer = 0;
            }
        }
    }

    public void Deliver(List<KitchenObjectSO> deliveredIngredients) {
        bool foundMatch = false;
        for (int i = 0; i < waitingRecipes.Count; ++i) {
            var waitingRecipe = waitingRecipes[i];
            HashSet<KitchenObjectSO> ingredientsSet =  ingredientsSetForRecipes[waitingRecipe.recipeName];
            // 原料数量要相同
            if (deliveredIngredients.Count != ingredientsSet.Count) continue;

            // 原料是否匹配
            bool match = true;
            foreach (KitchenObjectSO ingredient in deliveredIngredients) {
                if (!ingredientsSet.Contains(ingredient)) {
                    match = false;
                    break;
                }
            }

            if (match) {
                Debug.Log("Delivered a right recipe: " + waitingRecipe.recipeName);
                foundMatch = true;
                waitingRecipes.RemoveAt(i);
                break;
            }
        }

        if (!foundMatch) {
            Debug.Log("Delivered a wrong recipe: ");
        }
    }
}
