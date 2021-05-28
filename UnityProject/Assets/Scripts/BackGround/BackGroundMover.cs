using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackGroundMover : MonoBehaviour
{
    [SerializeField] private float _targetY;
    [SerializeField] private float _duration;

    private Group[] _groups;

    private void Awake()
    {
        _groups = GetComponentsInChildren<Group>();
    }

    private void Start()
    {
        foreach (var group in _groups)
        {
            Transform position = group.gameObject.transform;
            position.DOMoveY(position.position.y + _targetY, _duration)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }
    }
}
