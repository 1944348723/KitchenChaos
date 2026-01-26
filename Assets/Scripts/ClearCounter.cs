using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact() {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;

        kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetCounter(this);
    }
}
