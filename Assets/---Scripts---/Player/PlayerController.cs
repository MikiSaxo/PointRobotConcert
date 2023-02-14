using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _smoke;
    
    public void Move(Vector2 goalPos)
    {
        if (CanvasInventory.Instance.IsInventoryOpen) return;

        Vector2 pos = transform.position;
        var distance = Vector2.Distance(pos, goalPos);
        transform.DOKill();
        transform.DOMoveX(goalPos.x, distance / _speed).SetEase(Ease.Linear);

        ChangeDirection(goalPos.x);
    }

    private void ChangeDirection(float goalPos)
    {
        if (goalPos - transform.position.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            var ps = _smoke.main;
            ps.startSpeed = new ParticleSystem.MinMaxCurve(1, 3);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);

            var ps = _smoke.main;
            ps.startSpeed = new ParticleSystem.MinMaxCurve(-1, -3);
        }
    }
}