using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject _effect = null;
    public GameObject Effect { set { _effect = value; } }

    private Damage _target = null;
    private float _damage = 0.0f;

    private float _speed = 10.0f;
    public float Speed { set { _speed = value; } }

    private Vector3 _targetPos;

    private float _lifeTime = 0.0f;
    public float LifeTime { set { _lifeTime = value; } }

    private bool _bHit = false;

    private bool _bHiming = false;
    public bool Homing { set { _bHiming = value; } }

    private GameObject _spawnedEffect = null;

    void Start()
    {
        this.transform.LookAt(_targetPos);

        Invoke("DestroyArrow", _lifeTime);//특정시간에 호출
    }

    void Update()
    {
        if (_bHit == true) return;//화살이 몸에 붙어있는 상태
        if(_bHiming == true)
        {
            CalcTargetPos();
            this.transform.LookAt(_targetPos);
        }

        this.transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        _spawnedEffect.transform.position = this.transform.position;
    }

    public void SetTarget(Damage target, float damage)
    {
        _target = target;
        CalcTargetPos();
        _damage = damage;

    }

    private void CalcTargetPos()
    {
        Vector3 position = _target.transform.position;
        position.y = this.transform.position.y;
        _targetPos = position;
    }

    private void DestroyArrow()
    {
        Destroy(this.gameObject);
        Destroy(_spawnedEffect);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_bHit == true) return;
        if (other.GetComponent<Damage>() == null) return;

        if(other.tag != "Player")
        {
            other.GetComponent<Damage>().Hitted(_damage);
            AttachArrow(other);
            _bHit = true;
        }
    }

    private void AttachArrow(Collider col)
    {
        this.transform.parent = col.transform;
        _spawnedEffect.transform.parent = col.transform;
    }
}
