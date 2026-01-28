using System.Linq;
using UnityEngine;

public class StoveCounter : MonoBehaviour, IInteractable, IKitchenObjectParent {
    private enum StoveState {
        Idle,
        Frying,
        Burning,
        Burned
    };

    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private FryingRecipeSO[] fryingRecipes;
    [SerializeField] private BurningRecipeSO[] burningRecipes;

    private KitchenObject kitchenObject;
    private StoveState currentState = StoveState.Idle;
    private FryingRecipeSO currentFryingRecipeSO;
    private BurningRecipeSO currentBurningRecipeSO;
    private float fryingTimer = 0f;
    private float burningTimer = 0f;

    private void Update() {
        switch (currentState) {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                HandleFrying();
                break;
            case StoveState.Burning:
                HandleBurning();
                break;
            case StoveState.Burned:
                break;
        }
    }

    public void Interact(Player player) {
        if (kitchenObject && !player.HasKitchenObject()) {
            kitchenObject.SetParent(player);
            currentState = StoveState.Idle;
        } else if (!kitchenObject && player.HasKitchenObject() && HasFryingRecipeFor(player.GetKitchenObject().GetKitchenObjectSO())) {
            player.GetKitchenObject().SetParent(this);

            currentFryingRecipeSO = GetFryingRecipeFor(kitchenObject.GetKitchenObjectSO());
            currentBurningRecipeSO = GetBurningRecipeFor(currentFryingRecipeSO.output);
            fryingTimer = 0f;
            burningTimer = 0f;
            currentState = StoveState.Frying;
        }
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject) {
        if (this.kitchenObject) {
            Debug.Log("StoveCounter already has a kitchenobject");
            return;
        }

        kitchenObject.transform.parent = counterTopPoint.transform;
        kitchenObject.transform.localPosition = Vector3.zero;
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
        this.counterTopPoint.DetachChildren();
    }

    public bool HasKitchenObject() {
        return this.kitchenObject != null;
    }

    public KitchenObject GetKitchenObject() {
        return this.kitchenObject;
    }

    private bool HasFryingRecipeFor(KitchenObjectSO input) {
        return GetFryingRecipeFor(input) != null;
    }

    private FryingRecipeSO GetFryingRecipeFor(KitchenObjectSO input) {
        return fryingRecipes.FirstOrDefault(recipe => recipe.input == input);
    }

    private BurningRecipeSO GetBurningRecipeFor(KitchenObjectSO input) {
        return burningRecipes.FirstOrDefault(recipe => recipe.input == input);
    }

    private void HandleFrying() {
        fryingTimer += Time.deltaTime;
        if (fryingTimer >= currentFryingRecipeSO.timeNeeded) {
            fryingTimer = 0;
            kitchenObject.DestroySelf();
            KitchenObject.Spawn(currentFryingRecipeSO.output, this);

            currentState = StoveState.Burning;
            burningTimer = 0f;
        }
    }

    private void HandleBurning() {
        burningTimer += Time.deltaTime;
        if (burningTimer >= currentBurningRecipeSO.timeNeeded) {
            burningTimer = 0f;
            kitchenObject.DestroySelf();
            KitchenObject.Spawn(currentBurningRecipeSO.output, this);

            currentState = StoveState.Burned;
        }
    }
}
