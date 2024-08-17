using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter {

	public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
	public class OnProgressChangedEventArgs: EventArgs {
		public float progressNormalized;
	}
	
	public event EventHandler OnCut; 
	
	[SerializeField] private CuttingRecipeSO[] cutKitchenObjectSos;

	private int cuttingProgress;

	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			if (player.HasKitchenObject()) {
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo())) {
					player.GetKitchenObject().SetKitchenObjectParent(this);
					cuttingProgress = 0;
					var cuttingRecipeSo = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
					OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs() { progressNormalized = (float) cuttingProgress / cuttingRecipeSo.cuttingProgressMax });
				}
			}
		}
		else {
			if (player.HasKitchenObject()) {
			}
			else {
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}

	public override void InteractAlternative(Player player) {
		if (HasKitchenObject()) {
			var cuttingRecipeSo = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
			if (cuttingRecipeSo == null) {
				return;
			}

			cuttingProgress++;
			OnCut.Invoke(this, EventArgs.Empty);
			OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs() { progressNormalized = (float) cuttingProgress/cuttingRecipeSo.cuttingProgressMax });
			if (cuttingRecipeSo.cuttingProgressMax <= cuttingProgress) {
				GetKitchenObject().DestroySelf();
				KitchenObject.Spawn(cuttingRecipeSo.output, this);
			}
		}
	}

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
		CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSoWithInput(inputKitchenObjectSO);
		if (cuttingRecipeSo != null) {
			return cuttingRecipeSo.output;
		}

		return null;
	}

	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo) {
		return GetCuttingRecipeSoWithInput(inputKitchenObjectSo) != null;
	}

	private CuttingRecipeSO GetCuttingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo) {
		foreach (var cutKitchenObjectSo in cutKitchenObjectSos) {
			if (cutKitchenObjectSo.input == inputKitchenObjectSo) {
				return cutKitchenObjectSo;
			}
		}

		return null;
	}
}