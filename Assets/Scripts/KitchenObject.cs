using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

   public void DestroySelf() {
      _kitchenObjectParent.ClearKitchenObject();
      Destroy(gameObject);
   }

   public static KitchenObject Spawn(KitchenObjectSO kitchenObjectSo, IKitchenObjectParent kitchenObjectParent) {
      Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
      KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
      kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
      return kitchenObject;
   }
}
