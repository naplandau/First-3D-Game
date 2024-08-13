using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabedObject;
    [SerializeField] protected KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.Spawn(kitchenObjectSo, this);
            OnPlayerGrabedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}