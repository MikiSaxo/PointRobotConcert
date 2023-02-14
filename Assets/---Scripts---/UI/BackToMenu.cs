using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] private Image _bgFade;
    
    public void OnClickBackToMenu()
    {
        _bgFade.gameObject.SetActive(true);
        _bgFade.DOFade(1, 3f).OnComplete(ChangeSceneToMenu);
    }

    private void ChangeSceneToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
