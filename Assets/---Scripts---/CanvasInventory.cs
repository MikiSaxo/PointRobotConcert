using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInventory : MonoBehaviour
{
    [SerializeField] private GameObject _prefabItem;
    [SerializeField] private GameObject _panel;

    public void AddItem(Item item)
    {
        GameObject newItem = Instantiate(_prefabItem, _panel.transform, false);
        newItem.GetComponent<ItemUi>().Initialize(item);
    }
}
