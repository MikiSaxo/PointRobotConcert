using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private Image _iconItem;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private float _timeToWait;
    [SerializeField] private float _timeToDespawn;

    private bool _hasAlreadyAnim;
    private bool _stopCoroutine;

    public void InitNewItem(Sprite icon, string desc)
    {
        _iconItem.sprite = icon;
        _description.text = desc;

        if (_hasAlreadyAnim)
            _stopCoroutine = true;

        StartCoroutine(AnimNewItem());
        _hasAlreadyAnim = true;
    }

    IEnumerator AnimNewItem()
    {
        // transform.DOComplete();
        transform.DOKill();
        transform.DOScale(0, 0);

        transform.DOScale(1, _timeToSpawn);
        yield return new WaitForSeconds(_timeToWait);

        if (_stopCoroutine)
        {
            ForceResetAnim();
            yield break;
        }

        transform.DOScale(1.25f, .1f);
        yield return new WaitForSeconds(.15f);

        if (_stopCoroutine)
        {
            ForceResetAnim();
            yield break;
        }

        transform.DOScale(0, _timeToDespawn);

        _hasAlreadyAnim = false;
        _stopCoroutine = false;
    }

    private void ForceResetAnim()
    {
        transform.DOKill();
        transform.DOScale(0, _timeToDespawn);
    }
}