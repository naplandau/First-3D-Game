using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveGameObject;
    [SerializeField] private GameObject particleGameObject;

    private void Start() {
        stoveCounter.OnStageChanged += StoveCounterOnOnStageChanged;
    }

    private void StoveCounterOnOnStageChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool _showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveGameObject.SetActive(_showVisual);
        particleGameObject.SetActive(_showVisual);
    }
}
