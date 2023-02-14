using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Image _bgFade;

    private void Start()
    {
        AudioManager.Instance.PlaySound("Concert");
    }

    public void OnClickPlay()
    {
        // AudioManager.Instance.PlaySound("Button");
        _bgFade.gameObject.SetActive(true);
        _bgFade.DOFade(1, 1).OnComplete(ChangeScene);
    }

    private void ChangeScene()
    {
        AudioManager.Instance.StopSound("Concert");
        AudioManager.Instance.PlaySound("Ambiance");
        SceneManager.LoadScene("--InitScene--");
    }
}
