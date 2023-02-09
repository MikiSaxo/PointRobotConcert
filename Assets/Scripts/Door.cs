using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _nextScene;

    public void Execute()
    {
        PlayerPrefs.SetString(GameManager.NextSceneKey, _nextScene);
        SceneManager.LoadScene("GameCommon");
    }
}
