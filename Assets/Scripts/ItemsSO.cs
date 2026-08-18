using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsSO", menuName = "Scriptable Objects/ItemsSO")]
public class ItemsSO : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public GameObject InventoryPrefab; //Inventory Object
    public GameObject ItemPrefab; //In-Game Object

    [System.Serializable]
    public struct Combination
    {
        public string itemA;          // base name before the '_' (e.g. "Stick (Broken)")
        public string itemB;          // other item (order is ignored)
        public string resultPrefab;   // Resources path to resulting prefab (e.g. "Stick (Fixed)")
    }

    public Combination[] combinations;

}
