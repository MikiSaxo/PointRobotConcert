using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private TMP_Text _text; 
    private void Awake()
    {
        Instance = this;
    }

    public void ActivateDialogue(string text)
    {
        CanvasInventory.Instance.IsDialogueOpen = true;
        transform.DOScale(0,0);
        transform.DOScale(1, .5f);

        _text.text = $"<incr>{text}";
    }

    public void DeactivateDialogue()
    {
        CanvasInventory.Instance.IsDialogueOpen = false;
        transform.DOScale(0,.5f);
    }
}
