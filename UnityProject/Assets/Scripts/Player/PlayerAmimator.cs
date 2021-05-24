using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAmimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ChangeIsGround(bool value)
    {
        _animator.SetBool("Ground", value);
    }

    public void ChandeVerticalVelosity(float value)
    {
        _animator.SetFloat("VerticalVelosity", value);
    }

    public void ChangeMoved(bool value)
    {
        _animator.SetBool("Run", value);
    }

    public void ChangeDoubleJump()
    {
        _animator.SetTrigger("DoubleJump");
    }

    public void ChangeHit()
    {
        _animator.SetTrigger("Hit");
    }

    public void ChangeWall(bool value)
    {
        _animator.SetBool("Wall", value);
    }

    public void ChangeIsDeath()
    {
        _animator.SetTrigger("Destroy");
    }
}
