using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnIngrediantAddedEventAgrs> OnIngredientAdded;
    public class OnIngrediantAddedEventAgrs: EventArgs {
        public KitchenObjectSO kitchenObjectSo;

    }
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSos;
    private List<KitchenObjectSO> kitchenObjectSos;
    
    
    private void Awake() {
        kitchenObjectSos = new List<KitchenObjectSO>();
    }
    
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo) {
        if (!validKitchenObjectSos.Contains(kitchenObjectSo)) {
            return false;
        }
        
        if (kitchenObjectSos.Contains(kitchenObjectSo)) {
            return false;
        }
        else {
            kitchenObjectSos.Add(kitchenObjectSo);
            OnIngredientAdded?.Invoke(this, new OnIngrediantAddedEventAgrs() { kitchenObjectSo = kitchenObjectSo });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSos() {
        return kitchenObjectSos;
    }
}
