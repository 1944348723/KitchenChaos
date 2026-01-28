using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatesCounterVisual : CounterVisual {
    [SerializeField] private KitchenObjectSO plateSO;
    [SerializeField] private Transform counterTopPoint;

    private float GAP_BETWEEN_PLATES = 0.1f;
    private List<GameObject> plates = new();

    private new void Start() {
        base.Start();
        (counter as PlatesCounter).OnPlateSpawned += HandlePlateSpawned;
        (counter as PlatesCounter).OnPlateRemoved += HandlePlateRemoved;
    }

    private void HandlePlateSpawned() {
        var plateTransform = Instantiate(plateSO.prefab, counterTopPoint);
        plateTransform.localPosition = new (0, plates.Count * GAP_BETWEEN_PLATES, 0);
        plates.Add(plateTransform.gameObject);
    }

    private void HandlePlateRemoved() {
        var plateToRemove = plates.Last();
        plates.RemoveAt(plates.Count - 1);
        Destroy(plateToRemove);
    }
}
