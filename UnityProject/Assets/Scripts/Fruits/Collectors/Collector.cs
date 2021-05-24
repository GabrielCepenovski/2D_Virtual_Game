using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    public UnityEvent _collected = new UnityEvent();
    public int CountCollected => _countCollected;

    private int _countCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<CollectedObject>(out CollectedObject collectedObject))
        {
            _countCollected++;
            _collected.Invoke();
            collectedObject.PlayerConnect();
        }
    }
}
