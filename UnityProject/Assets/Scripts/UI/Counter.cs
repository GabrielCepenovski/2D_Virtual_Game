using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Counter : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private float _duration;

    public void OnChange(int _count)
    {
        _text.DOText(_count.ToString(), _duration);
    }
}
