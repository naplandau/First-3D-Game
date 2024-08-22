using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    
    public static event EventHandler OnAnyObjectPlaceHere;
    
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject _kitchenObject;
    
    public virtual void Interact(Player player)
    {
        
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {

        this._kitchenObject = kitchenObject;
        if (kitchenObject != null) {
            OnAnyObjectPlaceHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this._kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }

    public virtual void InteractAlternative(Player player) {
    }
}
