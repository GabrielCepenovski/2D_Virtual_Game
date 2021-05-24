using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class CollectorJumpFruit : Collector
{
    private PlayerMover _playerMover;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<JumpFruit>(out JumpFruit jumpFruit))
        {
            _playerMover.ResetJumpIndex();
            _collected.Invoke();
            jumpFruit.PlayerConnect();
        }
    }
}
