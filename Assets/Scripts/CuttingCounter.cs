using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

   [SerializeField] private KitchenObjectSO cutKitchenObjectSo;
   
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
         }
         else
         {
            GetKitchenObject().SetKitchenObjectParent(player);
         }
      }
   }
   
   public override void InteractAlternative(Player player) {
      if (HasKitchenObject()) {
         GetKitchenObject().DestroySelf();
         KitchenObject.Spawn(cutKitchenObjectSo, this);
      }
   }
}
