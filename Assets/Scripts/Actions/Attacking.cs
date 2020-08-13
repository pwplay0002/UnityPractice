﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour, IAction
{
    [SerializeField, Range(1.0f, 100.0f)]
    private float _power = 5.0f;

    [SerializeField, Range(1.0f, 3.0f)]
    private float _range = 2.0f;

    [SerializeField, Range(1.0f, 5.0f)]
    private float _delay = 2.0f;//공격 텀
    private float _time_sinces_last_attacked = 0.0f;//공격한 이래로 걸린 시간

    private Animator _animator = null;

    private ActionManager _actionManager = null;
    private Moving _moving = null;
    private Damage _target = null;

    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _moving = this.GetComponent<Moving>();
        _actionManager = this.GetComponent<ActionManager>();
    }

    void Update()
    {
        if (_target == null)
            return;

        if(_target.Death == true)
        {
            _animator.ResetTrigger("Attack");
            return;
        }

        if(IsInRange() == true)
        {
            _moving.End();
            PlayAnimation();
        }
        else
        {
            _moving.MoveTo(_target.transform.position);
        }
    }

    public void Begin(object obj)
    {
        Damage damage = obj as Damage;//obj를 Damage로 캐스팅이 가능하냐
        Debug.Assert((damage == null), "Input Type : Damage");

        _actionManager.StartAction(this);
        _target = damage;

        _time_sinces_last_attacked = _delay;
    }

    public void End()
    {
        _target = null;
        _animator.ResetTrigger("Attack");
    }

    public bool IsCanAttack(GameObject target)
    {
        if (target == null)
            return false;

        Damage damage = target.GetComponent<Damage>();
        return damage != null && damage.Death == false;
    }

    private void PlayAnimation()
    {
        this.transform.LookAt(_target.transform);//해당방향으로 돌림
        _time_sinces_last_attacked += Time.deltaTime;

        if (_time_sinces_last_attacked < _delay)
            return;

        _animator.SetTrigger("Attack");
        _time_sinces_last_attacked = 0.0f;
    }

    private bool IsInRange()//공격범위 안에 들어있냐
    {
        Vector2 a = new Vector2(_target.transform.position.x, _target.transform.position.z);
        Vector2 b = new Vector2(this.transform.position.x, this.transform.position.z);

        return Vector2.Distance(a, b) < _range;
    }

    private void Hit()
    {
        if (_target == null)
            return;

        _target.Hitted(_power);
    }
}