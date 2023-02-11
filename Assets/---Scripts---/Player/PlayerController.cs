using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Move(Vector2 goalPos)
    {
        if (CanvasInventory.Instance.IsInventoryOpen) return;
        
        Vector2 pos = transform.position;
        var distance = Vector2.Distance(pos, goalPos);
        transform.DOKill();
        transform.DOMoveX(goalPos.x, distance / _speed).SetEase(Ease.Linear);

        GetComponentInChildren<SpriteRenderer>().flipX = goalPos.x - transform.position.x > 0;
    }
}