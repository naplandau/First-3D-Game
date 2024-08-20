using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class RecipeSO : ScriptableObject {
    public List<KitchenObjectSO> kitchenObjectSos;
    public string recipeName;
}
