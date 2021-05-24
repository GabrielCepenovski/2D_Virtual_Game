using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    public UnityEvent _collected = new UnityEvent();
    public int CountCollected { get; private set; }

    private void Awake()
    {
        CountCollected = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Fruit>(out Fruit collectedObject))
        {
            CountCollected++;
            _collected.Invoke();
            collectedObject.PlayerConnect(gameObject);
        }
    }
}
