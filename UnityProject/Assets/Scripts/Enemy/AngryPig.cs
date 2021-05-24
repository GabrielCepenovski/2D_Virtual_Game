using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AngryPig : MonoBehaviour
{
    [SerializeField] private int _health = 2;
    [SerializeField] private GameObject _patrolPath;
    [SerializeField] private float _patrolSpead = 2f;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _waitDestroy = 0.6f;

    public int Health => _health;
    public bool IsAlive => _health > 0;

    private Rigidbody2D _rigibody2d;
    private PathPoint[] _points;
    private int _curentPoint;
    private Twisted _twisted = Twisted.Left;

    private void Awake()
    {
        if(_patrolPath != null)
        {
            _points = _patrolPath.GetComponentsInChildren<PathPoint>();
        }

        _rigibody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        PatrolMove();
    }

    private void PatrolMove()
    {
        if (IsAlive)
        {

            Transform target = _points[_curentPoint].transform;
            transform.position = Vector3.MoveTowards(transform.position, target.position, _patrolSpead * Time.deltaTime);

            if (transform.position == target.position)
            {
                _curentPoint++;

                if (_twisted == Twisted.Left)
                {
                    _twisted = Twisted.Right;
                    transform.rotation = new Quaternion(0,180f,0,0);
                }
                else
                {
                    _twisted = Twisted.Left;
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }

                if (_curentPoint >= _points.Length)
                {
                    _curentPoint = 0;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>( out Player movment) && IsAlive)
        {
            if (transform.position.y < movment.transform.position.y)
            {
                _health--;
                _animator.SetTrigger("Hit");
                _patrolSpead++;
                _animator.SetInteger("Health",_health);

                if (_health <= 0)
                    StartCoroutine(Death());
            }
            else
            {
                movment.GetHit();
            }
        }
    }

    private IEnumerator Death()
    {
        _rigibody2d.simulated = false;
        yield return new WaitForSeconds(_waitDestroy);
        Destroy(gameObject);
    }
}
