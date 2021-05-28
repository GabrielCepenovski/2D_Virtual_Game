using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _stepsValue = 0.02f;
    [SerializeField] private float _jumpForce = 4f;
    [SerializeField] private int _additionalJumps = 1;
    [SerializeField] private float _jumpDelay = 0.2f;
    [SerializeField] private float _wallSpead = -1f;
    [SerializeField] private UnityEvent _onJump;

    public bool IsGround { get; private set; }
    public float VerticalVelosity { get; private set; }
    public bool IsRun { get; private set; }
    public bool IsWall { get; private set; }
    public float StepsValue => _stepsValue;

    private Rigidbody2D _rigidbody2d;
    private Player _player;
    private int _jumpIndex = 0;
    private float _jumpDelayTimer = 0;
    private Vector3 _lastPosition;
    private PlayerAmimator _playerAmimator;
    private bool _lastIsGround;
    private bool _lastIsRun;
    private bool _lastIsWall;
    private float _lastverticalvelocity;

    public void ResetJumpIndex()
    {
        _jumpIndex = 0;
        _jumpDelayTimer = _jumpDelay + 0.1f;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerAmimator = GetComponent<PlayerAmimator>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(_player.IsAlive)
        {
            Move();
            Jump();
            Wall();
        }

        ParametrsUpdate();
    }

    private void Move()
    {
        RotationAndMove();

        void RotationAndMove()
        {
            float angle = 0;
            if (_player.Twisted == Twisted.Left)
                angle = 180f;

            if (_player.Twisted == Twisted.Right)
                angle = 0;

            transform.rotation = new Quaternion(0, angle, 0, 0);
            if (Input.GetAxis("Horizontal") != 0)
            {
                if (!_player.CheckContacts(Vector3.right * (int)_player.Twisted))
                {
                    transform.position += Vector3.right * (int)_player.Twisted * _stepsValue;
                }
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (IsGround)
            {
                _rigidbody2d.velocity = Vector2.zero;
                _rigidbody2d.velocity += Vector2.up * _jumpForce;
                _jumpDelayTimer = 0;

                _onJump.Invoke();
            }
            else if (_jumpDelayTimer > _jumpDelay && _jumpIndex < _additionalJumps)
            {
                _rigidbody2d.velocity = Vector2.zero;
                _rigidbody2d.velocity += Vector2.up * _jumpForce;
                _jumpIndex++;
                _jumpDelayTimer = 0;

                _onJump.Invoke();
            }
        }
    }

    private void Wall()
    {
        if (_jumpDelayTimer > _jumpDelay && IsWall == true && IsGround == false)
        {
            if (_player.Twisted == Twisted.Left && Input.GetKey(KeyCode.A) || _player.Twisted == Twisted.Right && Input.GetKey(KeyCode.D))
            {
                _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _wallSpead);

                if ( !_player.CheckContacts(Vector3.right * (int)_player.Twisted))
                {
                    _jumpDelayTimer = 0;
                }
            }
        }
    }

    private void ParametrsUpdate()
    {
        if (_player.CheckContacts(Vector3.down))
            ResetJumpIndex();

        _lastverticalvelocity = VerticalVelosity;
        IsRun = _lastPosition.x != transform.position.x;
        VerticalVelosity = _rigidbody2d.velocity.y;
        IsGround = _player.CheckContacts(Vector3.down);
        IsWall = _player.CheckContacts(Vector3.right * (int)_player.Twisted) && _jumpDelayTimer > _jumpDelay;

        if (!IsGround && _jumpDelayTimer == 0 && _jumpIndex > 0)
            _playerAmimator.ChangeDoubleJump();

        if (_lastIsWall != IsWall)
            _playerAmimator.ChangeWall(IsWall);

        if (_lastIsGround != IsGround)
            _playerAmimator.ChangeIsGround(IsGround);

        if (_lastIsRun != IsRun)
            _playerAmimator.ChangeMoved(IsRun);

        if (VerticalVelosity != _lastverticalvelocity)
            _playerAmimator.ChandeVerticalVelosity(VerticalVelosity);

        _lastIsRun = IsRun;
        _lastIsGround = IsGround;
        _lastIsWall = IsWall;

        _lastPosition = transform.position;
        _jumpDelayTimer += Time.deltaTime;
    }
}