using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [field:SerializeField] public string Name { get; private set; }

    private void Start()
    {
        if(CanvasInventory.Instance.IsObjectAlreadyPickUp(this.Name))
            Destroy(gameObject);
    }

    public void Execute()
    {
        GameManager.Instance.AddItem(this);
        Destroy(gameObject);
    }
}
