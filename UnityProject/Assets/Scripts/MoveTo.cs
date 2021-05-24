using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTo : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _time;

    private Vector3 _targetLastPosition;
    private Tweener _tween;

    private void Start()
    {
        Vector3 _targetPosition = ReplacementToZ(_target.position, transform.position.z);
        _tween = transform.DOMove(_targetPosition, _time).SetAutoKill(false);

        _targetLastPosition = ReplacementToZ(_target.position, transform.position.z);
    }

    private void Update()
    {
        Vector3 _targetPosition = ReplacementToZ(_target.position, transform.position.z);

        if (_targetLastPosition != _targetPosition)
        {
            _tween.ChangeEndValue(_targetPosition, true).Restart();
            _targetLastPosition = ReplacementToZ(_target.position, transform.position.z);
        }
    }

    private Vector3 ReplacementToZ(Vector3 value, float z)
    {
        return new Vector3(value.x, value.y, z);
    }
}
