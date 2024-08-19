using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] protected KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        KitchenObject playerKitchenObject = player.GetKitchenObject();
                        if (plateKitchenObject.TryAddIngredient(playerKitchenObject.GetKitchenObjectSo())) {
                            playerKitchenObject.DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}