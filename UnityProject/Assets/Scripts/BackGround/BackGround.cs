using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;

    private Group[] _groups;

    private void Awake()
    {
        _groups = GetComponentsInChildren<Group>();
    }

    private void Update()
    {
        foreach (var group in _groups)
        {
            group.transform.position += _direction * Time.deltaTime;
        }
    }
}
