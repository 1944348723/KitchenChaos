using System.Linq;
using UnityEngine;

public class CuttingCounter : MonoBehaviour, IKitchenObjectParent, IInteractable, IHasProgress {
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;

    public event System.Action<float> OnProgressChanged;
    public event System.Action OnCut;

    private KitchenObject kitchenObject;
    private int cutTimes = 0;

    public void Interact(Player player) {
        if (!kitchenObject && player.HasKitchenObject() && HasOutputFor(player.GetKitchenObject().GetKitchenObjectSO())) {
            player.GetKitchenObject().SetParent(this);
            cutTimes = 0;

            int cutTimesNeeded = GetRecipeFor(kitchenObject.GetKitchenObjectSO()).cutTimesNeeded;
            OnProgressChanged?.Invoke((float)cutTimes / cutTimesNeeded);
        } else if (kitchenObject && !player.HasKitchenObject()) {
            kitchenObject.SetParent(player);
        } else if (kitchenObject && player.HasKitchenObject()) {
            // 玩家手里的是盘子
            if (player.GetKitchenObject() is Plate) {
                Plate plate = player.GetKitchenObject() as Plate;
                if (plate.TryAddIngredient(kitchenObject)) {
                    kitchenObject.DestroySelf();
                }
            }
        }
    }

    public void InteractAlternate(Player player) {
        if (kitchenObject && HasOutputFor(kitchenObject.GetKitchenObjectSO())) {
            KitchenObjectSO outputSO = GetOutputFor(kitchenObject.GetKitchenObjectSO());
            ++cutTimes;
            int cutTimesNeeded = GetRecipeFor(kitchenObject.GetKitchenObjectSO()).cutTimesNeeded;
            OnProgressChanged?.Invoke((float)cutTimes / cutTimesNeeded);
            OnCut?.Invoke();

            if (cutTimes >= cutTimesNeeded) {
                kitchenObject.DestroySelf();
                KitchenObject.Spawn(outputSO, this);
            }
        }
    }

    public KitchenObjectSO GetOutputFor(KitchenObjectSO input) {
        return cuttingRecipes.FirstOrDefault(cuttingRecipe => cuttingRecipe.input == input)?.output;
    }

    public bool HasOutputFor(KitchenObjectSO input) {
        return GetOutputFor(input) != null;
    }

    public CuttingRecipeSO GetRecipeFor(KitchenObjectSO input) {
        return cuttingRecipes.FirstOrDefault(cuttingRecipe => cuttingRecipe.input == input);
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        if (this.kitchenObject) {
            Debug.Log("CuttingCounter already has a kitchenobject");
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
}
