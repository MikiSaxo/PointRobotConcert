using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMove : MonoBehaviour
{
    [SerializeField] float frequency = 20f;
    [SerializeField] float magnitude = 0.5f;

    private float _time;

    void Start()
    {
        ResetTime();
    }

    void Update()
    {
        _time += Time.deltaTime;
        MoveUpNDown();
    }

    private void MoveUpNDown()
    {
        transform.position += transform.up * Mathf.Sin(_time * frequency) * magnitude;
    }

    private void ResetTime()
    {
        _time -= _time;
    }
}