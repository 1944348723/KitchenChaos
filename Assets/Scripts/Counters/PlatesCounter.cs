using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : MonoBehaviour, IInteractable {
    [SerializeField] KitchenObjectSO plateSO;

    public event System.Action OnPlateSpawned;
    public event System.Action OnPlateRemoved;

    private const int PLATES_MAX_COUNT = 4;
    private const float PLATE_SPAWN_INTERVAL = 4f;
    private float plateSpawnTimer = 0f;
    private int currentPlatesCount = 0;

    private void Update() {
        if (currentPlatesCount < PLATES_MAX_COUNT) {
            plateSpawnTimer += Time.deltaTime;
        }
        if (plateSpawnTimer >= PLATE_SPAWN_INTERVAL && currentPlatesCount < PLATES_MAX_COUNT) {
            plateSpawnTimer -= PLATE_SPAWN_INTERVAL;

            ++currentPlatesCount;
            if (currentPlatesCount == PLATES_MAX_COUNT) {
                plateSpawnTimer = 0;
            }
            OnPlateSpawned?.Invoke();
        }
    }

    public void Interact(Player player) {
        if (player.HasKitchenObject()) return;
        --currentPlatesCount;
        OnPlateRemoved?.Invoke();
        KitchenObject.Spawn(plateSO, player);
    }
}
