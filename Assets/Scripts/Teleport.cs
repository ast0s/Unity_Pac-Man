using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform oppositeSpawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position = collision.transform.position;
        position.x = oppositeSpawn.position.x;
        position.y = oppositeSpawn.position.y;
        collision.transform.position = position;
    }
}
