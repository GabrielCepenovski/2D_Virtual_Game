using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    public int CountCollected { get; private set; }

    public UnityEvent _collected = new UnityEvent();

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
            collectedObject.Collected(gameObject);
        }
    }
}
