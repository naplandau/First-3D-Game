using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance{get; private set;}
	[SerializeField] private AudioClipRefsSO audioClipRefsSo;

	private void Awake() {
		Instance = this;
	}
	private void Start() {
		DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
		DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
		CuttingCounter.OnAnyCut += CuttingCounterOnOnAnyCut;
		Player.Instance.OnPickSomething += Player_OnPickSomething;
		BaseCounter.OnAnyObjectPlaceHere += BaseCounter_OnAnyObjectPlaceHere;
		TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
	}

	private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e) {
		TrashCounter baseCounter = sender as TrashCounter;
		PlaySound(audioClipRefsSo.trash, baseCounter.transform.position);
	}

	private void BaseCounter_OnAnyObjectPlaceHere(object sender, EventArgs e) {
		BaseCounter baseCounter = sender as BaseCounter;
		PlaySound(audioClipRefsSo.objectDrop, baseCounter.transform.position);
	}

	private void Player_OnPickSomething(object sender, EventArgs e) {
		PlaySound(audioClipRefsSo.objectPickUp, Player.Instance.transform.position);
	}

	private void CuttingCounterOnOnAnyCut(object sender, EventArgs e) {
		CuttingCounter cuttingCounter = sender as CuttingCounter;
		PlaySound(audioClipRefsSo.chop, cuttingCounter.transform.position);
	}

	private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e) {
		PlaySound(audioClipRefsSo.deliveryFail, DeliveryManager.Instance.transform.position);
	}

	private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
		PlaySound(audioClipRefsSo.deliverySuccess, DeliveryManager.Instance.transform.position);
	}

	private void PlaySound(AudioClip clip, Vector3 position, float volume = 1f) {
		AudioSource.PlayClipAtPoint(clip, position, volume);
	}
	
	private void PlaySound(AudioClip[] clips, Vector3 position, float volume = 1f) {
		PlaySound(clips[UnityEngine.Random.Range(0, clips.Length)], position, volume);
	}

	public void PlayFootstepSound(Vector3 position, float volume = 2f) {
		PlaySound(audioClipRefsSo.footstep, position, volume);
	}
}
