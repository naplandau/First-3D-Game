using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;
    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0) {
            footstepTimer = footstepTimerMax;

            if (player.IsMoving()) {
                SoundManager.Instance.PlayFootstepSound(player.transform.position);
            }
        }
    }
}
