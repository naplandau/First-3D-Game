using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

   public event EventHandler OnStateChanged;
   
   public static GameManager Instance { get; private set; }
   private enum State {
      WaitingToStart,
      CountdownToStart,
      GamePlaying,
      GameOver
   }

   private State state;
   private float waitingToStartTimer = 1f;
   private float countdownToStartTimer = 3f;
   private float gamePlayingToStartTimer = 10f;
   private void Awake() {
      Instance = this;
      state = State.WaitingToStart;
   }

   private void Update() {
      switch (state) {
         case State.WaitingToStart:
            waitingToStartTimer -= Time.deltaTime;
            if (waitingToStartTimer <= 0) {
               state = State.CountdownToStart;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }

            break;
         case State.CountdownToStart:
            countdownToStartTimer -= Time.deltaTime;
            if (countdownToStartTimer <= 0) {
               state = State.GamePlaying;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }

            break;
         case State.GamePlaying:
            gamePlayingToStartTimer -= Time.deltaTime;
            if (gamePlayingToStartTimer <= 0) {
               state = State.GameOver;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }

            break;
         case State.GameOver:
            break;
      }
   }

   public bool IsGamePlaying() {
      return state == State.GamePlaying;
   }

   public bool IsCountdownToStartActive() {
      return state == State.CountdownToStart;
   }

   public float GetWaitingToStartTimer() {
      return waitingToStartTimer;
   }
}
