using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    
    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }
    
    public void SetRecipeSO(RecipeSO recipeSo) {
        recipeNameText.text = recipeSo.recipeName;

        foreach (Transform child in iconContainer) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        Debug.Log(recipeSo.kitchenObjectSos.Count);
        foreach (KitchenObjectSO kitchenObjectSo in recipeSo.kitchenObjectSos) {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSo.sprite;
        }
    }
}
