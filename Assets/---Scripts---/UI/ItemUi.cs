using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemUi : MonoBehaviour
{
    public void Initialize(Item item)
    {
        GetComponentInChildren<TMPro.TMP_Text>().text = item.Name;
        GetComponent<Image>().sprite = item.ItemImage;
        
        SpriteState ss = new SpriteState();
        ss.highlightedSprite = item.ItemSelectedImage;
        ss.selectedSprite = item.ItemSelectedImage;
        GetComponent<Button>().spriteState = ss;
    }
}
