using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer sr { get; private set; }
    public Movement movement { get; private set; }
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<Movement>();
    }
    private void Update()
    {
        if (movement.direction == Vector2.up)
            sr.sprite = up;
        else if (movement.direction == Vector2.down)
            sr.sprite = down;
        else if (movement.direction == Vector2.left)
            sr.sprite = left;
        else if (movement.direction == Vector2.right)
            sr.sprite = right;
    }
}
