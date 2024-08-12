using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject _kitchenObject;
    
    public void Interact(Player player)
    {
        if (_kitchenObject == null)
        {
            Transform kitchenObjectSoTransform = Instantiate(kitchenObjectSo.prefab, counterTopPoint);
            kitchenObjectSoTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            _kitchenObject.SetKitchenObjectParent(player);
        }

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {

        this._kitchenObject = kitchenObject;
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
}
