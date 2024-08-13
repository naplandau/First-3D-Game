using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] protected KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.Spawn(kitchenObjectSo, this);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}