using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CanvasInventory : MonoBehaviour
{
    public static CanvasInventory Instance;
    
    [HideInInspector] public bool SaveLastDoorSide { get; set; }
    [HideInInspector] public bool IsMouseOnUI { get; private set; }

    [SerializeField] private GameObject _prefabItem;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _inventoryButton;
    [SerializeField] private GameObject _backInventoryButton;
    [SerializeField] private Vector2 _timeOpenCloseInventory;

    private List<string> _allItemsPickedUp = new List<string>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("deja spawn canvas inven");
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "--InitScene--")
            SceneManager.LoadScene("GameCommon");
    }

    public void AddItem(Item item)
    {
        GameObject newItem = Instantiate(_prefabItem, _panel.transform, false);
        _allItemsPickedUp.Add(item.Name);
        newItem.GetComponent<ItemUi>().Initialize(item);
    }

    public bool IsObjectAlreadyPickUp(string item)
    {
        foreach (var itemP in _allItemsPickedUp)
        {
            if (itemP == item)
                return true;
        }
        return false;
    }

    public void OpenInventory()
    {
        _inventory.transform.DOKill();
        _inventory.transform.DOScale(1, _timeOpenCloseInventory.x);
        _inventoryButton.SetActive(false);
        _backInventoryButton.SetActive(true);
    }

    public void CloseInventory()
    {
        _inventory.transform.DOKill();
        _inventory.transform.DOScale(0, _timeOpenCloseInventory.y);
        _inventoryButton.SetActive(true);
        _backInventoryButton.SetActive(false);
    }

    public void IsMouseUI(bool which)
    {
        print("coucou : " + which);
        IsMouseOnUI = which;
    }
}
