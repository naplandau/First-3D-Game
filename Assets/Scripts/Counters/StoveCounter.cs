using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class StoveCounter : BaseCounter, IHasProgress {
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

	public event EventHandler<OnStateChangedEventArgs> OnStageChanged;

	public class OnStateChangedEventArgs : EventArgs {
		public State state;
	}

	public enum State {
		Idle,
		Frying,
		Fried,
		Burned
	}

	[SerializeField] private FryingReceipSO[] fryingRecipeSos;
	[SerializeField] private BurningRecipeSO[] burningRecipeSos;

	private State currentState;
	private float _fryingTimer;
	private float _burnTimer;
	private FryingReceipSO _fryingRecipeSo;
	private BurningRecipeSO _burningRecipeSo;

	private void Start() {
		currentState = State.Idle;
	}

	private void Update() {
		if (HasKitchenObject()) {
			switch (currentState) {
				case State.Idle:
					break;
				case State.Frying:
					_fryingTimer += Time.deltaTime;
					OnProgressChanged?.Invoke(this,
						new IHasProgress.OnProgressChangedEventArgs()
							{ progressNormalized = _fryingTimer / _fryingRecipeSo.fryingTimerMax });

					if (_fryingTimer >= _fryingRecipeSo.fryingTimerMax) {
						GetKitchenObject().DestroySelf();
						KitchenObject.Spawn(_fryingRecipeSo.output, this);

						_burningRecipeSo = getBurningRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
						currentState = State.Fried;
						OnStageChanged?.Invoke(this, new OnStateChangedEventArgs() { state = currentState });
						_burnTimer = 0f;
					}

					break;
				case State.Fried:
					_burnTimer += Time.deltaTime;
					OnProgressChanged?.Invoke(this,
						new IHasProgress.OnProgressChangedEventArgs()
							{ progressNormalized = _burnTimer / _burningRecipeSo.burningTimerMax });
					if (_burnTimer >= _burningRecipeSo.burningTimerMax) {
						GetKitchenObject().DestroySelf();
						KitchenObject.Spawn(_burningRecipeSo.output, this);
						currentState = State.Burned;
						OnStageChanged?.Invoke(this, new OnStateChangedEventArgs() { state = currentState });
						OnProgressChanged?.Invoke(this,
							new IHasProgress.OnProgressChangedEventArgs() { progressNormalized = 0f });
					}

					break;
				case State.Burned:
					break;
			}
		}
	}

	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			if (player.HasKitchenObject()) {
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo())) {
					player.GetKitchenObject().SetKitchenObjectParent(this);

					_fryingRecipeSo = GetFryingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());

					currentState = State.Frying;
					OnStageChanged?.Invoke(this, new OnStateChangedEventArgs() { state = currentState });
					_fryingTimer = 0f;
					OnProgressChanged?.Invoke(this,
						new IHasProgress.OnProgressChangedEventArgs()
							{ progressNormalized = _fryingTimer / _fryingRecipeSo.fryingTimerMax });
				}
			}
		}
		else {
			if (player.HasKitchenObject()) {
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo())) {
						GetKitchenObject().DestroySelf();

						currentState = State.Idle;
						OnStageChanged?.Invoke(this, new OnStateChangedEventArgs() { state = currentState });
						_fryingTimer = 0f;
						_burnTimer = 0f;
						OnProgressChanged?.Invoke(this,
							new IHasProgress.OnProgressChangedEventArgs() { progressNormalized = 0f });
					}
				}
			}
			else {
				GetKitchenObject().SetKitchenObjectParent(player);
				currentState = State.Idle;
				OnStageChanged?.Invoke(this, new OnStateChangedEventArgs() { state = currentState });
				_fryingTimer = 0f;
				_burnTimer = 0f;
				OnProgressChanged?.Invoke(this,
					new IHasProgress.OnProgressChangedEventArgs() { progressNormalized = 0f });
			}
		}
	}

	private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
		FryingReceipSO fryRecipeSo = GetFryingRecipeSoWithInput(inputKitchenObjectSO);
		if (fryRecipeSo != null) {
			return fryRecipeSo.output;
		}

		return null;
	}

	private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo) {
		return GetFryingRecipeSoWithInput(inputKitchenObjectSo) != null;
	}

	private FryingReceipSO GetFryingRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo) {
		foreach (var fryRecipeSo in fryingRecipeSos) {
			if (fryRecipeSo.input == inputKitchenObjectSo) {
				return fryRecipeSo;
			}
		}

		return null;
	}

	private BurningRecipeSO getBurningRecipeSoWithInput(KitchenObjectSO inputKitchenObjectSo) {
		foreach (var burningRecipeSo in burningRecipeSos) {
			if (burningRecipeSo.input == inputKitchenObjectSo) {
				return burningRecipeSo;
			}
		}

		return null;
	}
}