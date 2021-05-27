using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DestroyBounds : MonoBehaviour
{
    private Collider2D _rigidbody2d;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Collider2D>();
        _rigidbody2d.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
            Destroy(player.gameObject);
    }
}
