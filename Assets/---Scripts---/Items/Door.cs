using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _nextScene;
    [SerializeField] private bool _isLeft;
    
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public Sprite SelectedImage { get; private set; }
    
    private bool _clicked;
    private bool _hasTouchPlayer;

    public void Execute()
    {
        _clicked = true;
        CheckIfChangeScene();
        CanvasInventory.Instance.SaveLastDoorSide = _isLeft;
    }
    
    public void ResetClicked()
    {
        _clicked = false;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
        {
            _hasTouchPlayer = true;
            CheckIfChangeScene();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>())
            _hasTouchPlayer = false;
    }

    private void CheckIfChangeScene()
    {
        if(_hasTouchPlayer && _clicked)
            ChangeScene();
    }

    private void ChangeScene()
    {
        PlayerPrefs.SetString(GameManager.NextSceneKey, _nextScene);

        if (_nextScene == "4.Concert")
        {
            AudioManager.Instance.StopSound("Ambiance");
            AudioManager.Instance.PlaySound("Concert");
        }
        
        SceneManager.LoadScene("GameCommon");
    }

    public void OnPointerEnter()
    {
        GetComponent<SpriteRenderer>().sprite = SelectedImage;
        // AudioManager.Instance.PlaySound("Button");
    }

    public void OnPointerExit()
    {
        GetComponent<SpriteRenderer>().sprite = Image;
    }
    
    public bool GetHasClicked()
    {
        return _clicked;
    }
}
