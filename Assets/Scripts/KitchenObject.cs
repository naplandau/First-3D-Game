using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO kitchenObjectSo;

   private IKitchenObjectParent _kitchenObjectParent;
   public KitchenObjectSO GetKitchenObjectSo()
   {
      return kitchenObjectSo;
   }

   public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
   {
      if (this._kitchenObjectParent != null)
      {
         this._kitchenObjectParent.ClearKitchenObject();
      }

      this._kitchenObjectParent = kitchenObjectParent;
      kitchenObjectParent.SetKitchenObject(this);
      
      this._kitchenObjectParent = kitchenObjectParent;
      transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
      transform.localPosition = Vector3.zero;
   }

   public IKitchenObjectParent GetKitchenObjectParent()
   {
      return _kitchenObjectParent;
   }
}
