using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    [SerializeField] private float _maxMagnitude = 11;

    private Vector3 _startPosirion;

    private void Awake()
    {
        _startPosirion = transform.position;
    }

    private void Update()
    {
        if (transform.position.magnitude > _maxMagnitude)
        {
            transform.position = _startPosirion;
            Debug.Log('1');
        }
    }
}