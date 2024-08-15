using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter counter;
    [SerializeField] private GameObject[] visualGameObjects;
    
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == counter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    private void Show()
    {
        foreach (var visualGameObject in visualGameObjects)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var visualGameObject in visualGameObjects)
        {
            visualGameObject.SetActive(false);
        }
    }
}
