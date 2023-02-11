using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabrikManager : MonoBehaviour
{
    public static FabrikManager Instance;
    
    [SerializeField] private GameObject _prefabItem;
    private List<string> _allItemsFabrik = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item item)
    {
        GameObject newItem = Instantiate(_prefabItem, transform, false);
        _allItemsFabrik.Add(item.ItemName);
        newItem.GetComponent<ItemUi>().Initialize(item, true);
    }

    public void RemoveFromFabrik(string name)
    {
        _allItemsFabrik.Remove(name);
    }
}
