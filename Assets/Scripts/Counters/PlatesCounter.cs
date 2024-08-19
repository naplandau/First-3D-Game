using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {
	
	public event EventHandler OnPlateSpawn;
	public event EventHandler OnPlateRemoved;
	
	[SerializeField] private KitchenObjectSO plateKitchenObjectSo;

	private float spawnTimer = 0f;
	private float spawnTimerMax = 4f;
	private int platesSpwanAmount;
	private int platesSpwanAmountMax = 4;
	private void Update() {
		spawnTimer += Time.deltaTime;
		if (spawnTimer >= spawnTimerMax) {
			spawnTimer = 0f;

			if (platesSpwanAmount < platesSpwanAmountMax) {
				platesSpwanAmount++;
				OnPlateSpawn?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public override void Interact(Player player) {
		if (!player.HasKitchenObject()) {
			if (platesSpwanAmount > 0) {
			
				platesSpwanAmount--;
				OnPlateRemoved?.Invoke(this, EventArgs.Empty);
				KitchenObject.Spawn(plateKitchenObjectSo, player);
			}	
		} 
	}
}
