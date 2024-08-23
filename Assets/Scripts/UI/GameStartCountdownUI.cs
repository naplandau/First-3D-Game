using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        Debug.Log($"state changed {GameManager.Instance.GetState()}");
        if (GameManager.Instance.IsCountdownToStartActive()) {
            Debug.Log("Starting Countdown");
            Show();
        }
        else {
            Debug.Log("Starting Game");
            Hide();
        }
    }

    private void Update() {
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer()).ToString();
    }
    
    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
