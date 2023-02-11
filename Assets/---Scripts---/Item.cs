using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite ItemImage { get; private set; }
    [field: SerializeField] public Sprite ItemSelectedImage { get; private set; }

    [SerializeField] private bool _isPermanent;
    [SerializeField] private Sprite _objSprite;
    [SerializeField] private Sprite _objSpriteSelected;

    private bool _isEmpty;
    private bool _hasTouchPlayer;
    private bool _clicked;

    private void Start()
    {
        if (CanvasInventory.Instance.IsObjectAlreadyPickUp(Name))
        {
            if (_isPermanent)
                _isEmpty = true;
            else
                Destroy(gameObject);
        }
    }

    public void Execute()
    {
        if (_isEmpty) return;

        _clicked = true;
        CheckIfGoToInventory();
    }

    private void CheckIfGoToInventory()
    {
        if (_hasTouchPlayer && _clicked)
            GoToInventory();
    }

    public void ResetClicked()
    {
        _clicked = false;
    }

    private void GoToInventory()
    {
        GameManager.Instance.AddItem(this);
        if (_isPermanent)
        {
            OnPointerExit();
            _isEmpty = true;
        }
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
        {
            _hasTouchPlayer = true;
            CheckIfGoToInventory();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
            _hasTouchPlayer = false;
    }

    public void OnPointerEnter()
    {
        // print("enter");
        if (_isEmpty) return;
        GetComponent<SpriteRenderer>().sprite = _objSpriteSelected;
    }

    public void OnPointerExit()
    {
        // print("leave");
        GetComponent<SpriteRenderer>().sprite = _objSprite;
    }

    public bool GetHasClicked()
    {
        return _clicked;
    }

}