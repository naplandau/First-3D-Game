using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
	public event EventHandler OnInteractAction;
	public event EventHandler OnInteractAlternativeAction;

	private PlayerInputAction playerInputAction;

	private void Awake() {
		playerInputAction = new PlayerInputAction();
		playerInputAction.Player.Enable();
		playerInputAction.Player.Interact.performed += InteractOnperformed;
		playerInputAction.Player.InteractAlternative.performed += InteractAlternativeOnperformed;
	}

	private void InteractAlternativeOnperformed(InputAction.CallbackContext obj) {
		OnInteractAlternativeAction?.Invoke(this, EventArgs.Empty);
	}

	private void InteractOnperformed(InputAction.CallbackContext obj) {
		OnInteractAction?.Invoke(this, EventArgs.Empty);
	}

	public Vector2 GetMovementVectorNormalized() {
		Vector2 input = playerInputAction.Player.Move.ReadValue<Vector2>();
		return input.normalized;
	}
}