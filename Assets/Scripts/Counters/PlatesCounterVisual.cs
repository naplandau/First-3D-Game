using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {
    

    [SerializeField] private PlatesCounter platesCounter;

    [SerializeField] private Transform counterTopPoint;

    [SerializeField] private Transform plateVisualPrefab;
    
    private List<GameObject> plateVisualGameObjects = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounterOnOnPlateSpawn;
        platesCounter.OnPlateRemoved += PlatesCounterOnOnPlateRemoved;
    }

    private void PlatesCounterOnOnPlateRemoved(object sender, EventArgs e) {
        GameObject plateGameObject = plateVisualGameObjects[plateVisualGameObjects.Count - 1];
        plateVisualGameObjects.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounterOnOnPlateSpawn(object sender, EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjects.Count, 0);
        plateVisualGameObjects.Add(plateVisualTransform.gameObject);
    }
}
