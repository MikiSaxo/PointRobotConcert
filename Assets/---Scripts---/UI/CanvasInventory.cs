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
    [HideInInspector] public bool IsInventoryOpen { get; private set; }
    [HideInInspector] public bool IsDialogueOpen { get; set; }
    [HideInInspector] public bool IsFabrikOpen { get; private set; }

    [Header("Setup")]
    [SerializeField] private GameObject _prefabItem;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _panelInventory;
    // [SerializeField] private GameObject _fabrik;
    // [SerializeField] private GameObject _panelfabrik;
    [SerializeField] private PopUpManager _popUpManager;
    [SerializeField] private Vector2 _timeOpenCloseInventory;
    [Header("Buttons")]
    [SerializeField] private GameObject _inventoryButton;
    [SerializeField] private GameObject _backInventoryButton;

    private List<string> _allItemsPickedUp = new List<string>();
    private bool _hasFirstDialogue;

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
        LaunchFirstDialogue();
    }

    public void AddItem(Item item, bool isFirstTime)
    {
        GameObject newItem = Instantiate(_prefabItem, _panelInventory.transform, false);
        _allItemsPickedUp.Add(item.ItemName);
        newItem.GetComponent<ItemUi>().Initialize(item, false);
        
        if(isFirstTime)
            _popUpManager.InitNewItem(item.ItemImage, item.ItemName, item.ItemMonologue);
    }

    public void RemoveItem(string name)
    {
        _allItemsPickedUp.Remove(name);
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
        IsInventoryOpen = true;
        _inventory.transform.DOKill();
        _inventory.transform.DOScale(1, _timeOpenCloseInventory.x);
        _inventoryButton.SetActive(false);
        _backInventoryButton.SetActive(true);
    }

    public void CloseInventory()
    {
        IsInventoryOpen = false;
        _inventory.transform.DOKill();
        _inventory.transform.DOScale(0, _timeOpenCloseInventory.y);
        _inventoryButton.SetActive(true);
        _backInventoryButton.SetActive(false);
    }

    public void IsMouseUI(bool which)
    {
        IsMouseOnUI = which;
    }

    private void Update()
    {
        if(IsDialogueOpen && Input.GetMouseButtonDown(0))
            DialogueManager.Instance.DeactivateDialogue();
    }

    private void LaunchFirstDialogue()
    {
        if (_hasFirstDialogue) return;
        DialogueManager.Instance.ActivateDialogue("There's a great Iron Maiden concert going on right now. I'll have to find a way to create a ticket");
        _hasFirstDialogue = true;
    }

    public void DeleteYourself()
    {
        Destroy(gameObject);
    }
}
