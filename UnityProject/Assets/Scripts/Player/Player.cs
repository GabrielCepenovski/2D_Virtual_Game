using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAmimator))]
[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private Twisted _twisted = Twisted.Left;
    [SerializeField] private float _damageBust = 6f;
    [SerializeField] private float _waitDestroy = 0.6f;
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private int _maxHealth = 4;
    [SerializeField] private UnityEvent _isDeath;
    [SerializeField] private UnityEvent<int> _changeHealth;
    [SerializeField] private UnityEvent _onHit;

    public Twisted Twisted => _twisted;
    public bool IsAlive => _health > 0;

    private Rigidbody2D _rigidbody2d;
    private PlayerAmimator _playerAmimator;
    private PlayerMover _playerMover;
    private Collector _collector;
    private readonly RaycastHit2D[] _hit = new RaycastHit2D[1];
    private int _health = 0;

    public void GetHit()
    {
        _playerAmimator.ChangeHit();
        _rigidbody2d.velocity = (Vector2.up + Vector2.left * (int)Twisted)  * _damageBust;

        _onHit.Invoke();
        _changeHealth.Invoke(--_health);

        if (_health<=0)
            StartCoroutine(Death());
    }

    public bool CheckContacts(Vector3 direction)
    {
        return _rigidbody2d.Cast(direction, _contactFilter, _hit, _playerMover.StepsValue) > 0;
    }

    public void ResetHealth()
    {
        _health = _maxHealth;
        _changeHealth.Invoke(_health);
    }

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _playerAmimator = GetComponent<PlayerAmimator>();
        _playerMover = GetComponent<PlayerMover>();
        _collector = GetComponent<Collector>();

        _health = _maxHealth;
    }

    private void FixedUpdate()
    {
        TwistedUpdate();

        if (IsAlive == false)
            _collector.enabled = false;
    }

    private void TwistedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
            _twisted = Twisted.Left;
        if (Input.GetKey(KeyCode.D))
            _twisted = Twisted.Right;
    }

    private IEnumerator Death()
    {
        _playerAmimator.ChangeIsDeath();
        _isDeath.Invoke();
        yield return new WaitForSeconds(_waitDestroy);
        Destroy(gameObject);
    }
}
