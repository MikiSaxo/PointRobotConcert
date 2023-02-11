using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Videur : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _objSprite;
    [SerializeField] private Sprite _objSpriteSelected;
    [SerializeField] private GameObject _blockConcert;
    private bool _clicked;

    public void Execute()
    {
        if (CanvasInventory.Instance.IsObjectAlreadyPickUp("Entry Ticket"))
        {
            _blockConcert.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void ResetClicked()
    {
        _clicked = false;
    }

    public void OnPointerEnter()
    {
        GetComponent<SpriteRenderer>().sprite = _objSpriteSelected;
    }

    public void OnPointerExit()
    {
        GetComponent<SpriteRenderer>().sprite = _objSprite;
    }

    public bool GetHasClicked()
    {
        return _clicked;
    }
}
