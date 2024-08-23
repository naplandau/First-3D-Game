using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DeliveryManager : MonoBehaviour {

	public event EventHandler OnRecipeSpawn;
	public event EventHandler OnRecipeCompleted;
	public event EventHandler OnRecipeFailed;
	public event EventHandler OnRecipeSuccess;
	
	public static DeliveryManager Instance { get; private set; }
	
	[SerializeField] private RecipeListSO recipeListSO;
	
	private List<RecipeSO> waitingRecipeSOList;

	private float spawnRecipeTimer;
	private float spawnRecipeTimerMax = 4f;
	private int waitingRecipeMax = 4;
	private void Awake() {
		Instance = this;
		waitingRecipeSOList = new List<RecipeSO>();
	}

	private void Update() {
		if (!GameManager.Instance.IsGamePlaying()) return;
		
		spawnRecipeTimer -= Time.deltaTime;
		if (spawnRecipeTimer <= 0f) {
			spawnRecipeTimer = spawnRecipeTimerMax;

			if (waitingRecipeMax > waitingRecipeSOList.Count) {

				RecipeSO waitingRecipeSo = recipeListSO.recipes[UnityEngine.Random.Range(0, recipeListSO.recipes.Count)];
				waitingRecipeSOList.Add(waitingRecipeSo);
				OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
		for (int i = 0; i < waitingRecipeSOList.Count; i++) {
			RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
			if (waitingRecipeSO.kitchenObjectSos.Count == plateKitchenObject.GetKitchenObjectSos().Count) {

				bool plateIngredientMatchs = true;
				foreach (var plateKitchenObjectSo in plateKitchenObject.GetKitchenObjectSos()) {
					bool ingredientFound = false;
					foreach (var waitingKitchenObjectSo in waitingRecipeSO.kitchenObjectSos) {
						if (plateKitchenObjectSo == waitingKitchenObjectSo) {
							ingredientFound = true;
							break;
						}
					}

					if (!ingredientFound) {
						plateIngredientMatchs = false;
						break;
					}
				}

				if (plateIngredientMatchs) {
					waitingRecipeSOList.RemoveAt(i);
					OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
					OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
					return;
				}
			}
		}
		OnRecipeFailed?.Invoke(this, EventArgs.Empty);
	}

	public List<RecipeSO> GetWaitingRecipeList() {
		return waitingRecipeSOList;
	}
}
