using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class CollectedObject : MonoBehaviour
{
    [SerializeField] private float _waitDestroy = 0.25f;

    public UnityEvent _collected;

    private Animator _animator;
    private Collider2D _colider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _colider = GetComponent<Collider2D>();

        _colider.isTrigger = true;
    }

    public void PlayerConnect()
    {
        StartCoroutine(Collected());
    }

    private IEnumerator Collected()
    {
        _animator.SetBool("Collected", true);
        _collected.Invoke();
        yield return new WaitForSeconds(_waitDestroy);
        Destroy(gameObject);
    }
}
