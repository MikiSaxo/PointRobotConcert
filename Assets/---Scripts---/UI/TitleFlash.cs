using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class TitleFlash : MonoBehaviour
{
    private Image _bg;
    [SerializeField] private float _timeFlash;
    void Start()
    {
        _bg = GetComponent<Image>();
        FlashOn();
    }

    private void FlashOn()
    {
        _bg.DOFade(.5f, _timeFlash).OnComplete(FlashOff).SetEase(Ease.Linear);
    }

    private void FlashOff()
    {
        _bg.DOFade(.1f, _timeFlash).OnComplete(FlashOn).SetEase(Ease.Linear);
    }
}
