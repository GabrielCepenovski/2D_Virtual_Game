using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class CollectorHealthFruit : Collector
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<HealthFruit>(out HealthFruit healthFruit))
        {
            _player.ResetHealth();
            _collected.Invoke();
            healthFruit.PlayerConnect();
        }
    }
}
