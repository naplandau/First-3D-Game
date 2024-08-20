using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour { 
	[SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private Transform iconTemplate;

	private void Awake() {
		iconTemplate.gameObject.SetActive(false);
	}
	
	private void Start() {
		plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
	}

	private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngrediantAddedEventAgrs e) {
		UpdateVisual();
	}

	private void UpdateVisual() {
		foreach (Transform child in transform) {
			if (child == iconTemplate) continue;
			Destroy(child.gameObject);
		}
		
		foreach (var kitchenObjectSo in plateKitchenObject.GetKitchenObjectSos()) {
			Transform iconTransform = Instantiate(iconTemplate, transform);
			iconTransform.gameObject.SetActive(true);
			iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSo);
		}
	}
}
