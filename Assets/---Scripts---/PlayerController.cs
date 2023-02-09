using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void Move(Vector2 position)
    {
        transform.position = position;
    }
}
