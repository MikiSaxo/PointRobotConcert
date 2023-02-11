using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FabrikManager : MonoBehaviour
{
    public static FabrikManager Instance;

    [SerializeField] private GameObject _transformButton;
    [SerializeField] private GameObject _prefabItem;
    [SerializeField] private List<ItemsConditions> _itemsConditions;
    [SerializeField] private GameObject[] _itemToFabrik;

    private Item _saveItemCanBeFabrik;

    private List<string> _allItemsFabrikString = new List<string>();
    private List<GameObject> _allItemsFabrikObj = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item item)
    {
        GameObject newItem = Instantiate(_prefabItem, transform, false);
        _allItemsFabrikString.Add(item.ItemName);
        _allItemsFabrikObj.Add(newItem);
        newItem.GetComponent<ItemUi>().Initialize(item, true);

        CheckIfCanFabrikNewItem();
    }

    public void RemoveFromFabrik(string namee)
    {
        _allItemsFabrikString.Remove(namee);
        foreach (var item in _allItemsFabrikObj)
        {
            if (item.GetComponent<ItemUi>().GetItemName() == namee)
            {
                _allItemsFabrikObj.Remove(item);
                break;
            }
        }

        CheckIfCanFabrikNewItem();
    }

    private void CheckIfCanFabrikNewItem()
    {
        var newItem = GetNewItem();
        if (newItem == String.Empty)
        {
            _saveItemCanBeFabrik = null;
            _transformButton.transform.DOScale(0, .3f);
        }
        else
        {
            foreach (var item in _itemToFabrik)
            {
                if (item.GetComponent<Item>().ItemName != newItem) continue;

                _saveItemCanBeFabrik = item.GetComponent<Item>();
                _transformButton.transform.DOScale(1, .3f);
                return;
            }
        }
    }

    public void OnTransformButton()
    {
        CanvasInventory.Instance.AddItem(_saveItemCanBeFabrik, true);
        foreach (var itemFabrik in _allItemsFabrikObj)
        {
            Destroy(itemFabrik);
        }

        _allItemsFabrikObj.Clear();
        _allItemsFabrikString.Clear();
        _saveItemCanBeFabrik = null;
        _transformButton.transform.DOScale(0, .3f);
    }

    private string GetNewItem()
    {
        foreach (var itemCollec in _itemsConditions)
        {
            if (itemCollec.Needed.Length == _allItemsFabrikString.Count)
            {
                var count = 0;
                foreach (var condi in itemCollec.Needed)
                {
                    if (_allItemsFabrikString.Contains(condi))
                        count++;
                }

                if (count == _allItemsFabrikString.Count)
                    return itemCollec.Result;
            }
        }

        return String.Empty;
    }
}