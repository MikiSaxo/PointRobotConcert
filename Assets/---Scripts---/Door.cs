using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _nextScene;
    [SerializeField] private bool _isLeft;
    private bool _clicked;

    public void Execute()
    {
        _clicked = true;
        CanvasInventory.Instance.SaveLastDoorSide = _isLeft;
    }
    
    public void ResetClicked()
    {
        _clicked = false;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerController>() && _clicked)
            ChangeScene();
    }

    private void ChangeScene()
    {
        PlayerPrefs.SetString(GameManager.NextSceneKey, _nextScene);
        SceneManager.LoadScene("GameCommon");
    }
}
