using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private CuttingRecipeSO[] cutKitchenObjectSos;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasRceipeWithInput(player.GetKitchenObject().GetKitchenObjectSo())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
        else {
            if (player.HasKitchenObject()) { }
            else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternative(Player player) {
        if (HasKitchenObject() && HasRceipeWithInput(GetKitchenObject().GetKitchenObjectSo())) {
            var outputKitchenObjSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSo());
            GetKitchenObject().DestroySelf();
            KitchenObject.Spawn(outputKitchenObjSo, this);
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (var cutKitchenObjectSo in cutKitchenObjectSos) {
            if (cutKitchenObjectSo.input == inputKitchenObjectSO) {
                return cutKitchenObjectSo.output;
            }
        }

        return null;
    }

    private bool HasRceipeWithInput(KitchenObjectSO inputKitchenObjectSo) {
        foreach (var cutKitchenObjectSo in cutKitchenObjectSos) {
            if (cutKitchenObjectSo.input == inputKitchenObjectSo) {
                return true;
            }
        }

        return false;
    }
}