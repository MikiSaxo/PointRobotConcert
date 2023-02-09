using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [field: SerializeField] public string Name { get; private set; }

    private bool _hasTouchPlayer;
    private bool _clicked;

    private void Start()
    {
        if (CanvasInventory.Instance.IsObjectAlreadyPickUp(Name))
            Destroy(gameObject);
    }

    public void Execute()
    {
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
}