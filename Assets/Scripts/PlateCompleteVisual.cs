using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlateCompleteVisual : MonoBehaviour {

    [Serializable]
    public struct KitchentObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSo;
        public GameObject gameObject;
    }
    
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    [SerializeField] private List<KitchentObjectSO_GameObject> kitchentObjectSoGameObjects;
    
    // Start is called before the first frame update
    void Start() {   
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
        foreach (var kitchentObjectSoGameObject in kitchentObjectSoGameObjects) {
            kitchentObjectSoGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngrediantAddedEventAgrs e) {
        foreach (var kitchentObjectSoGameObject in kitchentObjectSoGameObjects) {
            if (kitchentObjectSoGameObject.kitchenObjectSo == e.kitchenObjectSo) {
                kitchentObjectSoGameObject.gameObject.SetActive(true);
            }
            
        }
        // e.kitchenObjectSo
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
