using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Execute();
    void ResetClicked();
    void OnPointerEnter();
    void OnPointerExit();
    bool GetHasClicked();
}
