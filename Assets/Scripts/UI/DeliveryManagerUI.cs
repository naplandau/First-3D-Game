using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {
	[SerializeField] private Transform container;
	[SerializeField] private Transform recipeTemplate;

	private void Awake() {
		recipeTemplate.gameObject.SetActive(false);
	}

	private void Start() {
		DeliveryManager.Instance.OnRecipeSpawn += DeliveryManager_OnRecipeSpawn;
		DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
	}

	private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e) {
		UpdateVisual();
	}

	private void DeliveryManager_OnRecipeSpawn(object sender, EventArgs e) {
		UpdateVisual();
	}

	private void UpdateVisual() {
		foreach (Transform child in container) {
			if (child == recipeTemplate) continue;
			Destroy(child.gameObject);
		}

		foreach (var recipeSo in DeliveryManager.Instance.GetWaitingRecipeList()) {
			Transform recipeTransform = Instantiate(recipeTemplate, container);
			recipeTransform.gameObject.SetActive(true);
			recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSo);
		}
	}
	
}
