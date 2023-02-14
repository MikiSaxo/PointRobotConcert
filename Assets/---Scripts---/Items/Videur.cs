using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Videur : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _objSprite;
    [SerializeField] private Sprite _objSpriteSelected;
    [SerializeField] private BoxCollider2D _doorConcert;
    private bool _clicked;

    public void Execute()
    {
        AudioManager.Instance.PlaySound("Button");
        if (CanvasInventory.Instance.IsObjectAlreadyPickUp("Entry Ticket"))
        {
            Disappear();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _doorConcert.enabled = true;
            DialogueManager.Instance.ActivateDialogue("Bouncer: Welcome and have a nice concert!");
        }
        else
        {
            DialogueManager.Instance.ActivateDialogue("Bouncer: You don't have an Entry Ticket, get away!");
        }
    }

    public void ResetClicked()
    {
        _clicked = false;
    }

    public void OnPointerEnter()
    {
        GetComponent<SpriteRenderer>().sprite = _objSpriteSelected;
        // AudioManager.Instance.PlaySound("Button");
    }

    public void OnPointerExit()
    {
        GetComponent<SpriteRenderer>().sprite = _objSprite;
    }

    public bool GetHasClicked()
    {
        return _clicked;
    }

    private void Disappear()
    {
        var spr = GetComponent<SpriteRenderer>();
        spr.DOFade(0, 2);
    }
}