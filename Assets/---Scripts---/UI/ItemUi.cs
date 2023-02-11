using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemUi : MonoBehaviour
{
    private Item _saveItem;
    private bool _isOnFabrik;
    
    public void Initialize(Item item, bool isFabrik)
    {
        _saveItem = item;
        _isOnFabrik = isFabrik;
        
        GetComponentInChildren<TMPro.TMP_Text>().text = item.ItemName;
        GetComponent<Image>().sprite = item.ItemImage;
        
        SpriteState ss = new SpriteState();
        ss.highlightedSprite = item.ItemSelectedImage;
        ss.selectedSprite = item.ItemSelectedImage;
        GetComponent<Button>().spriteState = ss;
    }
    
    public void TransformFabrik()
    {
        if (_isOnFabrik)
        {
            //Remove from fabrik
            CanvasInventory.Instance.AddItem(_saveItem, false);
            FabrikManager.Instance.RemoveFromFabrik(_saveItem.ItemName);
            Destroy(gameObject);
        }
        else
        {
            //Add to fabrik
            FabrikManager.Instance.AddItem(_saveItem);
            CanvasInventory.Instance.RemoveItem(_saveItem.ItemName);
            Destroy(gameObject);
        }
    }
}
