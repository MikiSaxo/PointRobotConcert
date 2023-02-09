using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasInventory : MonoBehaviour
{
    public static CanvasInventory Instance;
    
    [HideInInspector] public bool SaveLastDoorSide { get; set; }

    [SerializeField] private GameObject _prefabItem;
    [SerializeField] private GameObject _panel;

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
}
